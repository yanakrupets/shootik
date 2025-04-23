using System.Collections.Generic;
using System.Linq;
using Enums;
using Serializable;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Graphic Data", menuName = "Configs/Graphic Data")]
    public class GraphicData : ScriptableObject
    {
        [SerializeField] private OverlapGraphic[] overlapGraphics;
        
        [SerializeField] private Sprite[] backgroundSprites;
        [SerializeField] private Sprite[] landscapeBackgroundSprites;

        [SerializeField] private Sprite[] citizenSprites;
        [SerializeField] private Sprite[] enemySprites;

        public IEnumerable<OverlapGraphic> GetOverlapGraphic(LandscapeLayer layer) =>
            overlapGraphics
                .Where(overlap => (overlap.LandscapeLayer & (LandscapeLayerFlags)layer) != 0);

        public IReadOnlyCollection<AnimationData> GetAnimationData(OverlapType overlapType) =>
            overlapGraphics
                .Single(overlap => overlap.OverlapType == overlapType)
                .AnimationData;

        public Sprite GetRandomBackgroundSprite() =>
            backgroundSprites[Random.Range(0, backgroundSprites.Length)];

        public Sprite GetRandomLandscapeBackgroundSprite() =>
            landscapeBackgroundSprites[Random.Range(0, backgroundSprites.Length)];
        
        public Sprite GetRandomTargetSprite(TargetType targetType)
        {
            return targetType switch
            {
                TargetType.Citizen => citizenSprites[Random.Range(0, citizenSprites.Length)],
                TargetType.Enemy => enemySprites[Random.Range(0, enemySprites.Length)],
                _ => null
            };
        }
    }
}