using Serializable;

public class TargetPlace
{
    public AnimationData AnimationData { get; }

    public bool IsFree { get; set; }

    public TargetPlace(AnimationData animationData)
    {
        AnimationData = animationData;
        IsFree = true;
    }
}
