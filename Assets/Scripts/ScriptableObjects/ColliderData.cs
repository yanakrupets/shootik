using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Collider Data", menuName = "Configs/Collider Data")]
    public class ColliderData : ScriptableObject
    {
        [field: SerializeField] public List<ColliderPath> ColliderPaths { get; private set; }

        public ColliderPath this[OverlapType overlapType]
        {
            get { return ColliderPaths.FirstOrDefault(path => path.OverlapType == overlapType); }
        }
    }

    [Serializable]
    public class ColliderPath
    {
        [field: SerializeField] public OverlapType OverlapType { get; private set; }
        [field: SerializeField] public float Width { get; private set; }
        [field: SerializeField] public List<ColliderPathPoints> Paths { get; private set; }

        public ColliderPath(OverlapType overlapType, float width, List<ColliderPathPoints> paths)
        {
            OverlapType = overlapType;
            Width = width;
            Paths = paths;
        }
    }

    [Serializable]
    public class ColliderPathPoints
    {
        [field: SerializeField] public Vector2[] Points { get; private set; }

        public ColliderPathPoints(Vector2[] points)
        {
            Points = points;
        }
    }
}