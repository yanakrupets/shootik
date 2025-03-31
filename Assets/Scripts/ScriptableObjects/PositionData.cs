using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Position Data", menuName = "Configs/Position Data")]
    public class PositionData : ScriptableObject
    {
        [field: SerializeField] public OverlapPosition[] OverlapPositions { get; private set; }
        
        public bool TryGetValue(OverlapType overlapType, out OverlapPosition value)
        {
            value = OverlapPositions.SingleOrDefault(overlap => overlap.OverlapType == overlapType);
            return value != null;
        }
    }
}
