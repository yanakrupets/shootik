using Enums;
using UnityEngine;

namespace Controllers
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField] private Texture2D cursorTexture;
        [SerializeField] private Texture2D aimCursorTexture;
        [SerializeField] private Vector2 defaultHotspot = Vector2.zero;
        [SerializeField] private Vector2 aimHotspot = Vector2.zero;

        private void Start()
        {
            //ChangeView(CursorType.Aim);
        }

        public void ChangeView(CursorType cursorType)
        {
            switch (cursorType)
            {
                case CursorType.Default:
                    Cursor.SetCursor(cursorTexture, defaultHotspot, CursorMode.Auto);
                    break;
                case CursorType.Aim:
                    Cursor.SetCursor(aimCursorTexture, aimHotspot, CursorMode.Auto);
                    break;
            }
        }
    
        public void ChangeAimCursorView(Sprite sprite)
        {
        
        }
    }
}
