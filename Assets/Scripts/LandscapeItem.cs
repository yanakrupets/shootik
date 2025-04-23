using System.Collections.Generic;
using System.Linq;
using Enums;
using Interfaces;
using Serializable;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LandscapeItem : ShootableItem, ILandscapeItem
{
    private SpriteRenderer _spriteRenderer;

    // serialized particle system

    protected virtual void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public OverlapType OverlapType { get; private set; }

    public virtual void Initialize(
        OverlapType type,
        Sprite landscapeSprite, 
        Vector2 position, 
        string landscapeLayerName)
    {
        OverlapType = type;
        
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
