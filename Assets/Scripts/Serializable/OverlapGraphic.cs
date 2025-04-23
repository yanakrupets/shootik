using System;
using Enums;
using UnityEngine;

namespace Serializable
{
    [Serializable]
    public class OverlapGraphic
    {
        [SerializeField] private OverlapType overlapType;
        [SerializeField] private Sprite sprite;
        [SerializeField] private LandscapeLayerFlags landscapeLayer;
        [SerializeField] private AnimationData[] animationData;

        public OverlapType OverlapType => overlapType;
        public Sprite Sprite => sprite;
        public LandscapeLayerFlags LandscapeLayer => landscapeLayer;
        public AnimationData[] AnimationData => animationData;

        public float Width => sprite.bounds.size.x;
    }
}