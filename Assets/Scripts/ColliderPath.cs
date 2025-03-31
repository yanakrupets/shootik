using System;
using Enums;
using UnityEngine;

[Serializable]
public class ColliderPath
{
    [field: SerializeField] public OverlapType OverlapType { get; private set; }
    [field: SerializeField] public float Width { get; private set; }
    [field: SerializeField] public ColliderPathPoints[] Paths { get; private set; }

    public ColliderPath(OverlapType overlapType, float width, ColliderPathPoints[] paths)
    {
        OverlapType = overlapType;
        Width = width;
        Paths = paths;
    }
}