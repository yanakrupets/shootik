using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Canvases
{
    public class Result : MonoBehaviour, ICanvas
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private CanvasType canvasType;
        
        [Space]
        [SerializeField] private Button menuButton;

        public Canvas Canvas => canvas;
        public CanvasType CanvasType => canvasType;
        public Button MenuButton => menuButton;
    }
}
