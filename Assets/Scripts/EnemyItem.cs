using Interfaces;
using Serializable;
using UnityEngine;

public class EnemyItem : ShootableItem, ISpriteRenderer, IShotAnimated
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AnimationData shotAnimation;

    public AnimationData AnimationData => shotAnimation;
    
    public SpriteRenderer SpriteRenderer => spriteRenderer;

    protected override void PerformAttack()
    {
        base.PerformAttack();
        Debug.Log("Enemy shot");
        // blinking color animation
        // play sound
        // +point
        // return to pool
    }
}