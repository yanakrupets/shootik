using System.Collections.Generic;
using System.Linq;
using Serializable;
using UnityEngine;

public class TargetZone : LandscapeItem
{
    private TargetPlace[] _targetPlaces;

    public bool IsFree => _targetPlaces.Any(place => place.IsFree);
    
    public void GenerateTargetPlaces(IReadOnlyCollection<AnimationData> animationData)
    {
        _targetPlaces = new TargetPlace[animationData.Count];
        for (var i = 0; i < animationData.Count; i++)
        {
            _targetPlaces[i] = new TargetPlace(animationData.ElementAt(i));
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
