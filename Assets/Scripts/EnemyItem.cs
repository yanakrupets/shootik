using Interfaces;
using UnityEngine;

public class EnemyItem : TargetItem, IPoolableItem
{
    // serialized image
    
    protected override void PerformAttack()
    {
        Debug.Log("Enemy shot");
        // change image color to ?black?
        // hide item
    }

    public void OnGet()
    {
        Debug.Log("Enemy GET");
    }

    public void OnReturn()
    {
        Debug.Log("Enemy RETURN");
    }
}
