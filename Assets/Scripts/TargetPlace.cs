using UnityEngine;

public class TargetPlace : MonoBehaviour
{
    private TargetItem _item;
    
    public void SetItem(TargetItem item)
    {
        _item = item;
    }
    
    public void ShowTarget()
    {
        _item.gameObject.SetActive(true);
        // play animation
    }

    public void HideTarget()
    {
        // play animation
        _item.gameObject.SetActive(false);
        _item = null;
    }
}
