using System;

namespace Enums
{
    public enum LandscapeLayer
    {
        Back = LandscapeLayerFlags.Back,
        Overlap = LandscapeLayerFlags.Overlap,
        Front = LandscapeLayerFlags.Front,
    }

    [Flags]
    public enum LandscapeLayerFlags
    {
        Back = 1,
        Overlap = 2,
        Front = 4,
    }
}