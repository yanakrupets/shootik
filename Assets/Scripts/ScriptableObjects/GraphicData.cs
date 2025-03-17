using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Graphic Data", menuName = "Configs/Graphic Data")]
    public class GraphicData : ScriptableObject
    {
        [SerializeField] private List<OverlapGraphic> overlapGraphics;
        
        [SerializeField] private List<Sprite> backgroundSprites;
        [SerializeField] private List<Sprite> landscapeBackgroundSprites;

        public List<OverlapGraphic> GetOverlapGraphic(LandscapeLayer landscapeLayer)
        {
            return overlapGraphics
                .Where(overlap => (overlap.LandscapeLayer & (LandscapeLayerFlags)landscapeLayer) != 0)
                .ToList();
        }

        public Sprite GetRandomBackgroundSprite()
        {
            return backgroundSprites[Random.Range(0, backgroundSprites.Count)];
        }
        
        public Sprite GetRandomLandscapeBackgroundSprite()
        {
            return landscapeBackgroundSprites[Random.Range(0, backgroundSprites.Count)];
        }
    }

    [Serializable]
    public class OverlapGraphic
    {
        [field: SerializeField] public OverlapType OverlapType { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public LandscapeLayerFlags LandscapeLayer { get; private set; }
    }
}