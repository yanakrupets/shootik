using System.Collections.Generic;
using Enums;
using Serializable;
using UnityEngine;

namespace Interfaces
{
    public interface ILandscapeItem
    {
        OverlapType OverlapType { get; }
        
        void Initialize(
            OverlapType type,
            Sprite landscapeSprite, 
            Vector2 position, 
            string landscapeLayerName);
    }
}
