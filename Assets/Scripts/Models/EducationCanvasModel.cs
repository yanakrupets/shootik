using DI;
using UI.Canvases;

namespace Models
{
    public class EducationCanvasModel
    {
        public Education View { get; }

        [Inject]
        public EducationCanvasModel(Education view)
        {
            View = view;
        }
    }
}