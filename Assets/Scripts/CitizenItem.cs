using Interfaces;
using UnityEngine;

public class CitizenItem : TargetItem, IPoolableItem
{
    // serialized image
    
    protected override void PerformAttack()
    {
        Debug.Log("Citizen shot");
        // change image color to ?black?
        // hide item
    }

    public void OnGet()
    {
        Debug.Log("Citizen GET");
    }

    public void OnReturn()
    {
        Debug.Log("Citizen RETURN");
    }
}
