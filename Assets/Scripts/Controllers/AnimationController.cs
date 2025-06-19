using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Serializable;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers
{
    public class AnimationController
    {
        private static readonly Dictionary<Transform, Sequence> ActiveSequences = new();
        
        public static void Play(AnimationData animationData, Transform target, UnityAction callback = null)
        {
            StopAnimation(target);
            
            var sequence = DOTween.Sequence();
            ActiveSequences[target] = sequence;
            
            switch (animationData.AnimationType)
            {
                case AnimationType.Move:
                    target.transform.localPosition = animationData.StartPosition;
                    sequence
                        .Append(target
                            .DOLocalMove(animationData.EndPosition, animationData.MoveDuration)
                            .SetEase(Ease.OutCirc))
                        .AppendInterval(animationData.IntervalDuration)
                        .Append(target
                            .DOLocalMove(animationData.StartPosition, animationData.MoveDuration)
                            .SetEase(Ease.InCirc))
                        .OnComplete(() => callback?.Invoke());
                    break;
                case AnimationType.MoveAndRotate:
                    target.transform.localPosition = animationData.StartPosition;
                    sequence
                        .Append(target
                            .DOLocalMove(animationData.EndPosition, animationData.MoveDuration)
                            .SetEase(Ease.OutCirc))
                        .Join(target
                            .DOLocalRotate(new Vector3(0, 0, animationData.EndRotationZ), animationData.RotateDuration))
                        .AppendInterval(animationData.IntervalDuration)
                        .Append(target
                            .DOLocalMove(animationData.StartPosition, animationData.MoveDuration)
                            .SetEase(Ease.InCirc))
                        .Join(target
                            .DOLocalRotate(new Vector3(0, 0, animationData.StartRotationZ), animationData.RotateDuration))
                        .OnComplete(() => callback?.Invoke());
                    break;
                case AnimationType.ColorBlink:
                    if (target.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
                    {
                        spriteRenderer.color = Color.white;
                        sequence
                            .Append(spriteRenderer.DOColor(animationData.BlinkColor, animationData.BlinkDuration))
                            .OnComplete(() => callback?.Invoke());
                    }
                    break;
                case AnimationType.Scaling:
                    target.localScale = animationData.StartScale;
                    sequence
                        .Append(target.DOScale(animationData.EndScale, animationData.ScaleDuration))
                        .OnComplete(() =>
                        {
                            callback?.Invoke();
                            target.transform.localScale = animationData.StartScale;
                        });
                    break;
                case AnimationType.None:
                default:
                    break;
            }
            
            if (sequence != null && animationData.IsLoop)
            {
                sequence.SetLoops(animationData.LoopCount, animationData.LoopType);
            }
        }

        public static void StopAllAnimations(bool complete = false)
        {
            if (ActiveSequences.Count <= 0)
                return;

            foreach (var sequence in ActiveSequences)
            {
                if (sequence.Value.IsActive())
                {
                    sequence.Value.Kill(complete);
                }
            }
            ActiveSequences.Clear();
        }

        private static void StopAnimation(Transform target, bool complete = false)
        {
            if (ActiveSequences.TryGetValue(target, out var sequence))
            {
                if (sequence.IsActive())
                {
                    sequence.Kill(complete);
                }
                CleanupSequence(target);
            }
        }
        
        private static void CleanupSequence(Transform target)
        {
            if (ActiveSequences.ContainsKey(target))
            {
                ActiveSequences.Remove(target);
            }
        }
    }
}
