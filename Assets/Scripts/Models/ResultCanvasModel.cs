using DI;
using UI.Canvases;

namespace Models
{
    public class ResultCanvasModel
    {
        public Result View { get; }

        [Inject]
        public ResultCanvasModel(Result view)
        {
            View = view;
        }
    }
}
