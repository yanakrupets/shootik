using Interfaces;
using Serializable;
using UnityEngine;

public class CitizenItem : ShootableItem, ISpriteRenderer, IShotAnimated
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AnimationData shotAnimation;

    public AnimationData AnimationData => shotAnimation;
    
    public SpriteRenderer SpriteRenderer => spriteRenderer;
}
