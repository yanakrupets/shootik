using DG.Tweening;
using Enums;
using Serializable;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers
{
    public class AnimationController
    {
        public static void Play(AnimationData animationData, Transform target, UnityAction callback = null)
        {
            var sequence = DOTween.Sequence();
            switch (animationData.AnimationType)
            {
                case AnimationType.Move:
                    target.transform.localPosition = animationData.StartPosition;
                    target.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, animationData.StartRotationZ));
                    sequence
                        .Append(target
                            .DOLocalMove(animationData.EndPosition, 1f)
                            .SetEase(Ease.OutCirc))
                        .AppendInterval(1f)
                        .Append(target
                            .DOLocalMove(animationData.StartPosition, 1f)
                            .SetEase(Ease.InCirc))
                        .OnKill(() => callback?.Invoke());
                    break;
                case AnimationType.MoveAndRotate:
                    target.transform.localPosition = animationData.StartPosition;
                    target.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, animationData.StartRotationZ));
                    sequence
                        .Append(target
                            .DOLocalMove(animationData.EndPosition, 1f)
                            .SetEase(Ease.OutCirc))
                        .Join(target
                            .DOLocalRotate(new Vector3(0, 0, animationData.EndRotationZ), 1f))
                        .AppendInterval(1f)
                        .Append(target
                            .DOLocalMove(animationData.StartPosition, 1f)
                            .SetEase(Ease.InCirc))
                        .Join(target
                            .DOLocalRotate(new Vector3(0, 0, animationData.StartRotationZ), 1f))
                        .OnKill(() => callback?.Invoke());
                    break;
                case AnimationType.None:
                default:
                    break;
            }
        }
    }
}
