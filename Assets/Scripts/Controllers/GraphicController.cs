using System.Collections.Generic;
using System.Linq;
using DI;
using Enums;
using ScriptableObjects;
using Serializable;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class GraphicController
    {
        private readonly GraphicData _graphicData;

        private GraphicSpriteSet _enemySet;
        
        [Inject]
        public GraphicController(GraphicData graphicData)
        {
            _graphicData = graphicData;
        }

        public void SelectRandomEnemySet()
        {
            _enemySet = _graphicData.EnemySpriteSets[Random.Range(0, _graphicData.EnemySpriteSets.Length)];
        }
        
        public IEnumerable<OverlapGraphic> GetOverlapGraphic(LandscapeLayer layer) =>
            _graphicData.OverlapGraphics
                .Where(overlap => (overlap.LandscapeLayer & (LandscapeLayerFlags)layer) != 0);

        public IReadOnlyCollection<AnimationData> GetAnimationData(OverlapType overlapType) =>
            _graphicData.OverlapGraphics
                .Single(overlap => overlap.OverlapType == overlapType)
                .AnimationData;

        public Sprite GetRandomBackgroundSprite() =>
            _graphicData.BackgroundSprites[Random.Range(0, _graphicData.BackgroundSprites.Length)];

        public Sprite GetRandomLandscapeBackgroundSprite() =>
            _graphicData.LandscapeBackgroundSprites[Random.Range(0, _graphicData.LandscapeBackgroundSprites.Length)];
        
        public Sprite GetRandomTargetSprite(TargetType targetType)
        {
            return targetType switch
            {
                TargetType.Citizen => _graphicData.CitizenSprites[Random.Range(0, _graphicData.CitizenSprites.Length)],
                TargetType.Enemy => _enemySet.Sprites[Random.Range(0, _enemySet.Sprites.Length)],
                _ => null
            };
        }
    }
}