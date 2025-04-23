using System;
using Enums;
using UnityEngine;

namespace Serializable
{
    [Serializable]
    public class AimWeight
    {
        [field: SerializeField] public TargetType Type { get; private set; }
        [field: SerializeField, Range(0, 100)] public float Weight { get; private set; }
    }
}
