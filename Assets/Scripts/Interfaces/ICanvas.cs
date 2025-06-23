using Enums;
using UnityEngine;

namespace Interfaces
{
    public interface ICanvas
    {
        public Canvas Canvas { get; }
        public CanvasType CanvasType { get; }
        
        public void Open()
        {
            Canvas.enabled = true;
        }

        public void Close()
        {
            Canvas.enabled = false;
        }
    }
}
