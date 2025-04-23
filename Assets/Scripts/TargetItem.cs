using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class TargetItem : ShootableItem
{
    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void SetSprite(Sprite sprite)
    {
        if (sprite is null)
            return;
        
        _spriteRenderer.sprite = sprite;
    }

    public void ChangeFlipX(bool isOn)
    {
        _spriteRenderer.flipX = isOn;
    }
}
