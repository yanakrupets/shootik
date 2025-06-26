using DI;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ShootableItem : MonoBehaviour, IPointerClickHandler
{
    [Inject] protected EventManager EventManager;

    public bool IsShot { get; set; }

    protected virtual void PerformAttack()
    {
        if (!IsShot)
        {
            EventManager.PublishItemShot(this);
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        PerformAttack();
    }
}
