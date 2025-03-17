using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class LandscapeItem : ShootableItem
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private PolygonCollider2D _collider;
    
    // serialized particle system
    
    public void Initialize(List<ColliderPathPoints> paths, 
        Sprite sprite, 
        Vector2 position, 
        string layerName)
    {
        for (int i = 0; i < paths.Count; i++)
        {
            _collider.SetPath(i, paths[i].Points);
        }
        
        _spriteRenderer.sprite = sprite;
        transform.localPosition = position;
        _spriteRenderer.sortingLayerName = layerName;
    }
    
    protected override void PerformAttack()
    {
        Debug.Log("Landscape shot");
        // call particles
        // call sound
    }
}
