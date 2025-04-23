using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Serializable
{
    [Serializable]
    public class ColliderPath
    {
        [field: SerializeField] public OverlapType OverlapType { get; private set; }
        [field: SerializeField] public float Width { get; private set; }
    
        [SerializeField] private ColliderPathPoints[] paths;

        public IReadOnlyCollection<ColliderPathPoints> Paths => paths;

        public ColliderPath(OverlapType overlapType, float width, ColliderPathPoints[] paths)
        {
            OverlapType = overlapType;
            Width = width;
            this.paths = paths;
        }
    }
}