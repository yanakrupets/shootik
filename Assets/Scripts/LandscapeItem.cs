using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(PolygonCollider2D))]
public class LandscapeItem : ShootableItem, ILandscapeItem
{
    private SpriteRenderer _spriteRenderer;
    private PolygonCollider2D _polygonCollider2d;
    
    // serialized particle system

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _polygonCollider2d = GetComponent<PolygonCollider2D>();
    }

    public void Initialize(
        IReadOnlyCollection<ColliderPathPoints> landscapePaths, 
        Sprite landscapeSprite, 
        Vector2 position, 
        string landscapeLayerName)
    {
        for (var i = 0; i < landscapePaths.Count; i++)
        {
            _polygonCollider2d.SetPath(i, landscapePaths.ElementAt(i).Points);
        }
        
        _spriteRenderer.sprite = landscapeSprite;
        transform.localPosition = position;
        _spriteRenderer.sortingLayerName = landscapeLayerName;
    }
    
    protected override void PerformAttack()
    {
        Debug.Log("Landscape shot");
        // call particles
        // call sound
    }
}
