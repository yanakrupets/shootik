using Serializable;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Graphic Data", menuName = "Configs/Graphic Data")]
    public class GraphicData : ScriptableObject
    {
        [SerializeField] private OverlapGraphic[] overlapGraphics;
        
        [SerializeField] private Sprite[] backgroundSprites;
        [SerializeField] private Sprite[] landscapeBackgroundSprites;

        [SerializeField] private Sprite[] citizenSprites;
        [SerializeField] private GraphicSpriteSet[] enemySpriteSets;

        public OverlapGraphic[] OverlapGraphics => overlapGraphics;
        public Sprite[] BackgroundSprites => backgroundSprites;
        public Sprite[] LandscapeBackgroundSprites => landscapeBackgroundSprites;
        public Sprite[] CitizenSprites => citizenSprites;
        public GraphicSpriteSet[] EnemySpriteSets => enemySpriteSets;
    }
}