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

        public bool TryGetValue(OverlapType overlapType, out ColliderPath value)
        {
            value = ColliderPaths.SingleOrDefault(path => path.OverlapType == overlapType);
            return value != null;
        }
    }
}