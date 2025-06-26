using Interfaces;
using Serializable;
using UnityEngine;

public class HeartItem : ShootableItem, IShotAnimated
{
    [SerializeField] private AnimationData shotAnimation;

    public AnimationData AnimationData => shotAnimation;
}
