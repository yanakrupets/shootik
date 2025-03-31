using System.Collections.Generic;
using System.Linq;
using Enums;
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

        public IEnumerable<OverlapGraphic> GetOverlapGraphic(LandscapeLayer layer) =>
            overlapGraphics
                .Where(overlap => (overlap.LandscapeLayer & (LandscapeLayerFlags)layer) != 0);
        
        public OverlapGraphic[] GetOverlapGraphic(LandscapeLayerFlags flags) =>
            overlapGraphics
                .Where(overlap => (overlap.LandscapeLayer & flags) != 0)
                .ToArray();

        public Sprite GetRandomBackgroundSprite() =>
            backgroundSprites[Random.Range(0, backgroundSprites.Length)];

        public Sprite GetRandomLandscapeBackgroundSprite() =>
            landscapeBackgroundSprites[Random.Range(0, backgroundSprites.Length)];
    }
}