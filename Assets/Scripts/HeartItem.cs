using Interfaces;
using Serializable;
using UnityEngine;

public class HeartItem : ShootableItem, IShotAnimated
{
    [SerializeField] private AnimationData shotAnimation;

    public AnimationData AnimationData => shotAnimation;
    
    protected override void PerformAttack()
    {
        base.PerformAttack();
        Debug.Log("Heart shot");
        // transform animation
        // return to pool
        // play sound
        // +heart
    }
}
