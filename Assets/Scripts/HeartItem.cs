using UnityEngine;

public class HeartItem : ShootableItem
{
    protected override void PerformAttack()
    {
        Debug.Log("Heart shot");
    }
}
