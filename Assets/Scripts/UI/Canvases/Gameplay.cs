using Enums;
using Interfaces;
using UnityEngine;

namespace UI.Canvases
{
    public class Gameplay : MonoBehaviour, ICanvas
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private CanvasType canvasType;

        public Canvas Canvas => canvas;
        public CanvasType CanvasType => canvasType;
    }
}
