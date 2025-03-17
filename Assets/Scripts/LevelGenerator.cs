using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
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

    public void Start()
    {
        Generate();
    }

    public void Generate()
    {
        backgroundSpriteRenderer.sprite = graphicData.GetRandomBackgroundSprite();
        landscapeBackgroundSpriteRenderer.sprite = graphicData.GetRandomLandscapeBackgroundSprite();
        
        GenerateTargetZones();
        GenerateLandscape(LandscapeLayer.Back);
        GenerateLandscape(LandscapeLayer.Front);
    }

    private void GenerateTargetZones()
    {
        GenerateObjects(LandscapeLayer.Overlap, targetZonePrefab, 
            (zone, paths, sprite, position, layerName) =>
        {
            zone.Initialize(position);
            zone.LandscapeItem.Initialize(paths, sprite, Vector2.zero, layerName);
        });
    }
    
    private void GenerateLandscape(LandscapeLayer landscapeLayer)
    {
        GenerateObjects(landscapeLayer, landscapePrefab, 
            (item, paths, sprite, position, layerName) =>
            {
                item.Initialize(paths, sprite, position, layerName);
            });
    }

    private void GenerateObjects<TObject>(
        LandscapeLayer landscapeLayer, 
        TObject prefab,
        //action -> delegate
        Action<TObject, List<ColliderPathPoints>, Sprite, Vector2, string> initialize)
        where TObject : MonoBehaviour
    {
        var data = placementData.FirstOrDefault(data => data.landscapeLayer == landscapeLayer);
        
        var currentPosition = data.placementRange.x;
        
        while (true)
        {
            var overlapGraphic = GetRandomOverlapGraphic(landscapeLayer);
            
            var colliderWidth = colliderData[overlapGraphic.OverlapType].Width;
            
            if (currentPosition + colliderWidth > data.placementRange.y)
                break;
            
            var position = new Vector2(currentPosition + colliderWidth / 2, positionData[overlapGraphic.OverlapType].Y);
            var paths = colliderData[overlapGraphic.OverlapType].Paths;
            
            var item = Instantiate(prefab, data.transform);
            initialize(item, paths, overlapGraphic.Sprite, position, data.layerName);
            
            currentPosition += colliderWidth + data.distance;
        }
    }

    private OverlapGraphic GetRandomOverlapGraphic(LandscapeLayer landscapeLayer)
    {
        var graphic = graphicData.GetOverlapGraphic(landscapeLayer);
        //List<OverlapGraphic> graphic = graphicData.GetOverlapGraphic((LandscapeLayer)(LandscapeLayerFlags.Back | LandscapeLayerFlags.Front));
        var randomIndex = Random.Range(0, graphic.Count);
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
