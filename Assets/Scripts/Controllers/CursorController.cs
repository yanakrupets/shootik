using System;
using DI;
using Enums;
using Interfaces;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class CursorController : IInitializable, IDisposable
    {
        private Texture2D _cursorTexture;
        private Texture2D _aimCursorTexture;
        
        private readonly Vector2 _defaultHotspot = Vector2.zero;
        private Vector2 _crosshairHotspot;
        
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
            _aimCursorTexture = Resources.Load<Texture2D>("Sprites/Cursor_aim");

            if (_aimCursorTexture != null)
                _crosshairHotspot = new Vector2(_aimCursorTexture.width / 2, _aimCursorTexture.height / 2);

            ChangeView(GameState.None);
        }
        
        public void Dispose()
        {
            _eventManager.OnGameStateChanged -= ChangeView;
        }

        private void ChangeView(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Playing:
                    if (_aimCursorTexture != null)
                    {
                        Cursor.SetCursor(_aimCursorTexture, _crosshairHotspot, CursorMode.Auto);
                    }
                    break;
                case GameState.Paused:
                case GameState.None:
                    if (_cursorTexture != null)
                    {
                        Cursor.SetCursor(_cursorTexture, _defaultHotspot, CursorMode.Auto);
                    }
                    break;
            }
        }
    }
}
