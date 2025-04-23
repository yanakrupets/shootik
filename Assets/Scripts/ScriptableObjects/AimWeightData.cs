using System.Linq;
using UnityEngine;
using Serializable;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Aim Weight Data", menuName = "Configs/Aim Weight Data")]
    public class AimWeightData : ScriptableObject
    {
        public AimWeight[] weights;

        public float TotalWeight => weights.Sum(entry => entry.Weight);
    }
}
