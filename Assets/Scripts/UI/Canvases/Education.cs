using Enums;
using UnityEngine;
using Interfaces;
using UnityEngine.UI;

namespace UI.Canvases
{
    public class Education : MonoBehaviour, ICanvas
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
