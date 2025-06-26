using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Canvases
{
    public class PauseMenu : MonoBehaviour, ICanvas
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private CanvasType canvasType;

        [Space]
        [SerializeField] private Button continueButton;
        [SerializeField] private Button menuButton;
        
        public Canvas Canvas => canvas;
        public CanvasType CanvasType => canvasType;
        public Button ContinueButton => continueButton;
        public Button MenuButton => menuButton;
    }
}
