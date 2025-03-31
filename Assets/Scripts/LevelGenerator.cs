using System;
using System.Linq;
using Enums;
using Interfaces;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [Header("Background renderers")]
    [SerializeField] private SpriteRenderer backgroundSpriteRenderer;
    [SerializeField] private SpriteRenderer landscapeBackgroundSpriteRenderer;

    [Header("Prefabs")]
    [SerializeField] private TargetZone targetZonePrefab;
    [SerializeField] private LandscapeItem landscapePrefab;
    
    [Header("Configs")]
    [SerializeField] private GraphicData graphicData;
    [SerializeField] private ColliderData colliderData;
    [SerializeField] private PositionData positionData;

    [Space]
    [SerializeField] private PlacementData[] placementData;

    private const float LandscapePositionY = -4.4f;

    public void Start()
    {
        Generate();
    }

    public void Generate()
    {
        backgroundSpriteRenderer.sprite = graphicData.GetRandomBackgroundSprite();
        landscapeBackgroundSpriteRenderer.sprite = graphicData.GetRandomLandscapeBackgroundSprite();
        
        GenerateLandscape(LandscapeLayer.Back, landscapePrefab);
        GenerateLandscape(LandscapeLayer.Overlap, landscapePrefab);
        GenerateLandscape(LandscapeLayer.Front, landscapePrefab);
    }

    private void GenerateLandscape<TObject>(
        LandscapeLayer landscapeLayer, 
        TObject prefab)
        where TObject : MonoBehaviour, ILandscapeItem
    {
        var data = placementData.Single(data => data.landscapeLayer == landscapeLayer);
        
        var currentPosition = data.placementRange.x;
        
        while (true)
        {
            var overlapGraphic = GetRandomOverlapGraphic(landscapeLayer);
            
            if (!colliderData.TryGetValue(overlapGraphic.OverlapType, out var overlapCollider))
            {
                Debug.LogWarning("There is no such collider!");
                continue;
            }
            
            if (currentPosition + overlapCollider.Width > data.placementRange.y)
                break;
            
            var position = new Vector2(currentPosition + overlapCollider.Width / 2, LandscapePositionY);
            var paths = overlapCollider.Paths;
            
            var item = Instantiate(prefab, data.transform);
            item.Initialize(paths, overlapGraphic.Sprite, position, data.layerName);
            
            currentPosition += overlapCollider.Width + data.distance;
        }
    }

    private OverlapGraphic GetRandomOverlapGraphic(LandscapeLayer landscapeLayer)
    {
        var graphic = graphicData.GetOverlapGraphic(landscapeLayer).ToArray();
        var randomIndex = Random.Range(0, graphic.Length);
        return graphic[randomIndex];
    }

    [Serializable]
    private struct PlacementData
    {
        public LandscapeLayer landscapeLayer;
        public string layerName;
        public Transform transform;
        public Vector2 placementRange;
        public float distance;
    }
}
