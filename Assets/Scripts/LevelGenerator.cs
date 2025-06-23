using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using DI;
using Enums;
using ScriptableObjects;
using Serializable;
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

    [Space]
    [SerializeField] private PlacementData[] placementData;

    private ColliderData _colliderData;
    private GraphicController _graphicController;
    
    private const float LandscapePositionY = -4.4f;

    private List<LandscapeItem> _backItems;
    private List<LandscapeItem> _frontItems;
    private List<TargetZone> _targetZones;

    [Inject]
    public void Construct(ColliderData colliderData, GraphicController graphicController)
    {
        _colliderData = colliderData;
        _graphicController = graphicController;
    }

    public void Generate()
    {
        backgroundSpriteRenderer.sprite = _graphicController.GetRandomBackgroundSprite();
        landscapeBackgroundSpriteRenderer.sprite = _graphicController.GetRandomLandscapeBackgroundSprite();
        
        _backItems = GenerateLandscape(LandscapeLayer.Back, landscapePrefab);
        _frontItems = GenerateLandscape(LandscapeLayer.Front, landscapePrefab);
        
        _targetZones = GenerateLandscape(LandscapeLayer.Overlap, targetZonePrefab);
        SetupTargetZones();
    }

    public void Clean()
    {
        Clean(_backItems);
        Clean(_frontItems);
        Clean(_targetZones);
    }

    private void Clean<T>(List<T> items) where T : MonoBehaviour
    {
        foreach (var item in items)
        {
            Destroy(item.gameObject);
        }
        items.Clear();
    }

    public TargetZone GetRandomFreeTargetZone()
    {
        var freeZones = _targetZones
            .Where(zone => zone.IsFree)
            .ToArray();

        return freeZones.Length == 0 ? 
            null : 
            freeZones[Random.Range(0, freeZones.Length)];
    }

    private List<TObject> GenerateLandscape<TObject>(
        LandscapeLayer landscapeLayer, 
        TObject prefab)
        where TObject : LandscapeItem
    {
        var landscapeObjects = new List<TObject>();
        
        var data = placementData.Single(data => data.landscapeLayer == landscapeLayer);
        
        var currentPosition = data.placementRange.x;
        
        while (true)
        {
            var overlapGraphic = GetRandomOverlapGraphic(landscapeLayer);
            
            if (currentPosition + overlapGraphic.Width > data.placementRange.y)
                break;
            
            var position = new Vector2(currentPosition + overlapGraphic.Width / 2, LandscapePositionY);
            
            var item = DiFactory.Instantiate(prefab, data.transform);
            item.Initialize(overlapGraphic.OverlapType, overlapGraphic.Sprite, position, data.layerName);
            
            landscapeObjects.Add(item);
            
            currentPosition += overlapGraphic.Width + data.distance;
        }
        
        return landscapeObjects;
    }

    private void SetupTargetZones()
    {
        if (_targetZones == null)
        {
            Debug.LogError("There is no target zones!");
            return;
        }

        foreach (var zone in _targetZones)
        {
            var animationData = _graphicController.GetAnimationData(zone.OverlapType);
            zone.GenerateTargetPlaces(animationData);
            
            if (!_colliderData.TryGetValue(zone.OverlapType, out var overlapCollider))
            {
                Debug.LogError("There is no such collider!");
                continue;
            }
            var paths = overlapCollider.Paths;
            zone.SetPath(paths);
        }
    }

    private OverlapGraphic GetRandomOverlapGraphic(LandscapeLayer landscapeLayer)
    {
        var graphic = _graphicController.GetOverlapGraphic(landscapeLayer).ToArray();
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
