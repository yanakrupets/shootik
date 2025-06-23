using System.Collections.Generic;
using DI;
using Enums;
using Interfaces;
using UI.Canvases;
using UnityEngine;

namespace Controllers
{
    public class CanvasController
    {
        private ICanvas _currentCanvas;
        private readonly Dictionary<CanvasType, ICanvas> _canvases;
        
        [Inject]
        public CanvasController(
            Menu menuCanvas, 
            Gameplay gameplayCanvas, 
            PauseMenu pauseMenuCanvas, 
            Education educationCanvas, 
            Result resultCanvas)
        {
            _canvases = new Dictionary<CanvasType, ICanvas>();
            
            AddCanvas(menuCanvas);
            AddCanvas(gameplayCanvas);
            AddCanvas(pauseMenuCanvas);
            AddCanvas(educationCanvas);
            AddCanvas(resultCanvas);
        }

        public void Open(CanvasType canvasType)
        {
            if (canvasType == CanvasType.None)
            {
                Debug.LogError("Cannot open canvas type: None");
                return;
            }

            _currentCanvas?.Close();
        
            if (_canvases.TryGetValue(canvasType, out var canvas))
            {
                _currentCanvas = canvas;
                _currentCanvas.Open();
            }
            else
            {
                Debug.LogError($"No canvas with type ({canvasType}) exists");
            }
        }

        public void Close()
        {
            _currentCanvas?.Close();
            _currentCanvas = null;
        }
        
        private void AddCanvas(ICanvas canvas)
        {
            if (_canvases.TryAdd(canvas.CanvasType, canvas)) return;
            Debug.LogError($"Duplicate canvas type detected: {canvas.CanvasType}");
        }
    }
}
