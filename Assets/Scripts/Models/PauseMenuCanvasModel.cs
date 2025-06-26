using DI;
using UI.Canvases;

namespace Models
{
    public class PauseMenuCanvasModel
    {
        public PauseMenu View { get; }

        [Inject]
        public PauseMenuCanvasModel(PauseMenu view)
        {
            View = view;
        }
    }
}
