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
    
        [SerializeField] private ColliderPathPoints[] paths;

        public IReadOnlyCollection<ColliderPathPoints> Paths => paths;

        public ColliderPath(OverlapType overlapType, ColliderPathPoints[] paths)
        {
            OverlapType = overlapType;
            this.paths = paths;
        }
    }
}