using System;
using Enums;
using UnityEngine;

[Serializable]
public class OverlapPosition
{
    [field: SerializeField] public OverlapType OverlapType { get; private set; }
    [field: SerializeField] public float Y { get; private set; }
}