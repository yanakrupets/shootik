using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Position Data", menuName = "Configs/Position Data")]
    public class PositionData : ScriptableObject
    {
        [field: SerializeField] public List<OverlapPosition> OverlapPositions { get; private set; }

        public OverlapPosition this[OverlapType overlapType]
        {
            get { return OverlapPositions.FirstOrDefault(overlap => overlap.OverlapType == overlapType); }
        }
    }

    [Serializable]
    public class OverlapPosition
    {
        [field: SerializeField] public OverlapType OverlapType { get; private set; }
        [field: SerializeField] public float Y { get; private set; }
    }
}
