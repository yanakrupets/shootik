using Enums;
using UnityEngine;

namespace Controllers
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField] private Texture2D cursorTexture;
        [SerializeField] private Texture2D aimCursorTexture;
        
        private readonly Vector2 _defaultHotspot = Vector2.zero;
        private readonly Vector2 _crosshairHotspot = new Vector2(128, 128);

        private void Start()
        {
            ChangeView(CursorType.Aim);
        }

        public void ChangeView(CursorType cursorType)
        {
            switch (cursorType)
            {
                case CursorType.Default:
                    Cursor.SetCursor(cursorTexture, _defaultHotspot, CursorMode.Auto);
                    break;
                case CursorType.Aim:
                    Cursor.SetCursor(aimCursorTexture, _crosshairHotspot, CursorMode.Auto);
                    break;
            }
        }
    }
}
