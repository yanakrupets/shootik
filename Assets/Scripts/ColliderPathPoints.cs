using System;
using UnityEngine;

[Serializable]
public class ColliderPathPoints
{
    [field: SerializeField] public Vector2[] Points { get; private set; }

    public ColliderPathPoints(Vector2[] points)
    {
        Points = points;
    }
}