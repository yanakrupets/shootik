using System.Collections.Generic;
using UnityEngine;

public class TargetZone : MonoBehaviour
{
    [SerializeField] private LandscapeItem _landscapeItem;
    [SerializeField] private PolygonCollider2D _overlapCollider;

    public LandscapeItem LandscapeItem => _landscapeItem;
    
    private List<TargetPlace> _targetPlaces;

    public void Initialize(Vector2 position)
    {
        // set position
        // create SO for each overlap ( X random range + Y position )
        transform.position = position;

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
