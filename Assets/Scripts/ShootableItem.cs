using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ShootableItem : MonoBehaviour, IPointerClickHandler
{
    protected abstract void PerformAttack();
    
    public void OnPointerClick(PointerEventData eventData)
    {
        PerformAttack();
    }
}
