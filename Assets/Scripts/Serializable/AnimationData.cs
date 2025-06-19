using System;
using DG.Tweening;
using Enums;
using UnityEngine;

namespace Serializable
{
    [Serializable]
    public class AnimationData
    {
        [field: Header("Move")]
        [field: SerializeField] public float MoveDuration { get; private set; }
        [field: SerializeField] public Vector2 StartPosition { get; private set; }
        [field: SerializeField] public Vector2 EndPosition { get; private set; }
        
        [field: Header("Rotate")]
        [field: SerializeField] public float RotateDuration { get; private set; }
        [field: SerializeField] public float StartRotationZ { get; private set; }
        [field: SerializeField] public float EndRotationZ { get; private set; }
        
        [field: Header("Scale")]
        [field: SerializeField] public float ScaleDuration { get; private set; }
        [field: SerializeField] public Vector3 StartScale { get; private set; }
        [field: SerializeField] public Vector3 EndScale { get; private set; }
        
        [field: Header("Coloring")]
        [field: SerializeField] public float BlinkDuration { get; private set; }
        [field: SerializeField] public Color BlinkColor { get; private set; }
        
        [field: Header("Loop")]
        [field: SerializeField] public bool IsLoop { get; private set; }
        [field: SerializeField] public int LoopCount { get; private set; }
        [field: SerializeField] public LoopType LoopType { get; private set; }
        
        [field: Space]
        [field: SerializeField] public float IntervalDuration { get; private set; }
        [field: SerializeField] public AnimationType AnimationType { get; private set; }
    }
}
