using System;
using UnityEngine;

namespace Serializable
{
    [Serializable]
    public class GraphicSpriteSet
    {
        [SerializeField] private Sprite[] sprites;

        public Sprite[] Sprites => sprites;
    }
}