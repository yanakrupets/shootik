using UnityEngine;

namespace Interfaces
{
    public interface ILandscapeItem
    {
        void Initialize(
            ColliderPathPoints[] landscapePaths, 
            Sprite landscapeSprite, 
            Vector2 position, 
            string landscapeLayerName);
    }
}
