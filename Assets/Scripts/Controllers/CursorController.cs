using DI;
using Enums;
using Interfaces;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class CursorController : IInitializable
    {
        private Texture2D _cursorTexture;
        private Texture2D _aimCursorTexture;
        
        private readonly Vector2 _defaultHotspot = Vector2.zero;
        private readonly Vector2 _crosshairHotspot = new Vector2(128, 128);
        
        private readonly EventManager _eventManager;

        [Inject]
        public CursorController(EventManager eventManager)
        {
            _eventManager = eventManager;
        }
        
        public void Initialize()
        {
            _eventManager.OnGameStateChanged += ChangeView;
            
            _cursorTexture = Resources.Load<Texture2D>("Sprites/Cursor");
            _aimCursorTexture = Resources.Load<Texture2D>("Sprites/Cursor_0");
            
            ChangeView(GameState.None);
        }

        private void ChangeView(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Playing:
                    Cursor.SetCursor(_aimCursorTexture, _crosshairHotspot, CursorMode.Auto);
                    break;
                case GameState.Paused:
                case GameState.None:
                    Cursor.SetCursor(_cursorTexture, _defaultHotspot, CursorMode.Auto);
                    break;
            }
        }
    }
}
