using Enums;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] private Texture2D _cursorTexture;
    [SerializeField] private Texture2D _aimCursorTexture;
    [SerializeField] private Vector2 _defaultHotspot = Vector2.zero;
    [SerializeField] private Vector2 _aimHotspot = Vector2.zero;

    private void Start()
    {
        Cursor.SetCursor(_aimCursorTexture, _aimHotspot, CursorMode.Auto);
    }

    public void ChangeView(CursorType cursorType)
    {
        switch (cursorType)
        {
            case CursorType.Default:
                Cursor.SetCursor(_cursorTexture, _defaultHotspot, CursorMode.Auto);
                break;
            case CursorType.Aim:
                Cursor.SetCursor(_aimCursorTexture, _aimHotspot, CursorMode.Auto);
                break;
        }
    }
    
    public void ChangeAimCursorView(Sprite sprite)
    {
        
    }
}
