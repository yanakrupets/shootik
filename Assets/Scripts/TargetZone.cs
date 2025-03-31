using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

public class TargetZone : MonoBehaviour, ILandscapeItem
{
    [SerializeField] private LandscapeItem landscapeItem;
    
    private List<TargetPlace> _targetPlaces;

    public void Initialize(
        ColliderPathPoints[] landscapePaths, 
        Sprite landscapeSprite, 
        Vector2 position, 
        string landscapeLayerName)
    {
        // set position
        // create SO for each overlap ( X random range + Y position )
        transform.position = position;
        
        landscapeItem.Initialize(landscapePaths, landscapeSprite, Vector2.zero, landscapeLayerName);

        // generate target places
        // create SO for places for each overlap
    }

    public void SetTargetPlaces(List<TargetPlace> targetPlaces)
    {
        _targetPlaces = targetPlaces;
    }

    public void SetOverlap(Sprite sprite, Vector2 position)
    {
        //_overlapSpriteRenderer.sprite = sprite;
        //_overlapTransform.position = position;
    }
}
