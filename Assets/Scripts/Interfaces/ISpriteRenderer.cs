using JetBrains.Annotations;
using UnityEngine;

namespace Interfaces
{
    public interface ISpriteRenderer
    {
        public SpriteRenderer SpriteRenderer { get; }
    
        public void SetSprite([CanBeNull]Sprite sprite)
        {
            if (sprite is null)
                return;
        
            SpriteRenderer.sprite = sprite;
        }
    
        public void ChangeFlipX(bool isOn)
        {
            SpriteRenderer.flipX = isOn;
        }
    }
}
