using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Canvases
{
    public class Menu : MonoBehaviour, ICanvas
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private CanvasType canvasType;
        
        [Space]
        [SerializeField] private Button playButton;
        [SerializeField] private Button educationButton;

        public Canvas Canvas => canvas;
        public CanvasType CanvasType => canvasType;

        public Button PlayButton => playButton;
        public Button EducationButton => educationButton;
    }
}
