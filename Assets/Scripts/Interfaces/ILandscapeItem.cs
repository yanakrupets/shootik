using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface ILandscapeItem
    {
        void Initialize(
            IReadOnlyCollection<ColliderPathPoints> landscapePaths, 
            Sprite landscapeSprite, 
            Vector2 position, 
            string landscapeLayerName);
    }
}
