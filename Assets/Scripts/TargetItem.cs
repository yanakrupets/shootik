using UnityEngine;
using UnityEngine.UI;

public abstract class TargetItem : ShootableItem
{
    [SerializeField] private Image _image;

    public void SetImage(Image image)
    {
        _image = image;
    }
}
