using Enums;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LandscapeItem : ShootableItem
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
        base.PerformAttack();
        Debug.Log("Landscape shot");
        // particle
        // play sound
    }
}
