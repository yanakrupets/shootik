using System;
using Enums;
using UnityEngine;

[Serializable]
public class OverlapGraphic
{
    [field: SerializeField] public OverlapType OverlapType { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public LandscapeLayerFlags LandscapeLayer { get; private set; }
}