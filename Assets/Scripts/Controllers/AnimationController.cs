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
                        .Append(Move(target, animationData.EndPosition, animationData.MoveDuration, animationData.MoveStartEase))
                        .AppendInterval(animationData.IntervalDuration)
                        .Append(Move(target, animationData.StartPosition, animationData.MoveDuration, animationData.MoveEndEase))
                        .OnComplete(() => callback?.Invoke());
                    break;
                case AnimationType.MoveAndRotate:
                    target.transform.localPosition = animationData.StartPosition;
                    sequence
                        .Append(Move(target, animationData.EndPosition, animationData.MoveDuration, animationData.MoveStartEase))
                        .Join(Rotate(target, new Vector3(0, 0, animationData.EndRotationZ), animationData.RotateDuration))
                        .AppendInterval(animationData.IntervalDuration)
                        .Append(Move(target, animationData.StartPosition, animationData.MoveDuration, animationData.MoveEndEase))
                        .Join(Rotate(target, new Vector3(0, 0, animationData.StartRotationZ), animationData.RotateDuration))
                        .OnComplete(() => callback?.Invoke());
                    break;
                case AnimationType.ColorChange:
                    if (target.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
                    {
                        spriteRenderer.color = Color.white;
                        sequence
                            .Append(ChangeColor(spriteRenderer, animationData.Color, animationData.ColorDuration))
                            .OnComplete(() => callback?.Invoke());
                    }
                    break;
                case AnimationType.Scaling:
                    target.localScale = animationData.StartScale;
                    sequence
                        .Append(Scale(target, animationData.EndScale, animationData.ScaleDuration))
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

        private static Tween Move(Transform target, Vector2 targetPosition, float duration, Ease ease)
        {
            return target
                .DOLocalMove(targetPosition, duration)
                .SetEase(ease);
        }

        private static Tween Rotate(Transform target, Vector3 targetRotation, float duration)
        {
            return target
                .DOLocalRotate(targetRotation, duration);
        }

        private static Tween ChangeColor(SpriteRenderer target, Color color, float duration)
        {
            return target
                .DOColor(color, duration);
        }

        private static Tween Scale(Transform target, Vector3 scale, float duration)
        {
            return target
                .DOScale(scale, duration);
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
