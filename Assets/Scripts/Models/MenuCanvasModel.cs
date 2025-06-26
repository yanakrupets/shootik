using DI;
using UI.Canvases;

namespace Models
{
    public class MenuCanvasModel
    {
        public Menu View { get; }

        [Inject]
        public MenuCanvasModel(Menu view)
        {
            View = view;
        }
    }
}
