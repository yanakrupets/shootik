using System.Collections.Generic;
using System.Linq;
using Serializable;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class TargetZone : LandscapeItem
{
    private PolygonCollider2D _polygonCollider2d;
    private TargetPlace[] _targetPlaces;

    protected override void Awake()
    {
        base.Awake();
        _polygonCollider2d = GetComponent<PolygonCollider2D>();
    }

    public bool IsFree => _targetPlaces.Any(place => place.IsFree);
    
    public void GenerateTargetPlaces(IReadOnlyCollection<AnimationData> animationData)
    {
        _targetPlaces = new TargetPlace[animationData.Count];
        for (var i = 0; i < animationData.Count; i++)
        {
            _targetPlaces[i] = new TargetPlace(animationData.ElementAt(i));
        }
    }

    public void SetPath(IReadOnlyCollection<ColliderPathPoints> landscapePaths)
    {
        for (var i = 0; i < landscapePaths.Count; i++)
        {
            _polygonCollider2d.SetPath(i, landscapePaths.ElementAt(i).Points);
        }
    }

    public TargetPlace GetRandomFreeTargetPlace()
    {
        var freePlaces = _targetPlaces
            .Where(place => place.IsFree)
            .ToArray();
        
        return freePlaces.Length == 0 ? 
            null : 
            freePlaces[Random.Range(0, freePlaces.Length)];
    }
}
