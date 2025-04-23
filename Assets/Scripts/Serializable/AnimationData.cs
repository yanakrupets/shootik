using System;
using Enums;
using UnityEngine;

namespace Serializable
{
    [Serializable]
    public class AnimationData
    {
        [field: SerializeField] public Vector2 StartPosition { get; private set; }
        [field: SerializeField] public Vector2 EndPosition { get; private set; }
        [field: SerializeField] public float StartRotationZ { get; private set; }
        [field: SerializeField] public float EndRotationZ { get; private set; }
        [field: SerializeField] public AnimationType AnimationType { get; private set; }
    }
}
