using UnityEngine;

public class SteppedSliderAttribute : PropertyAttribute
{
    public float Min { get; }
    public float Max { get; }
    public float Step { get; }

    public SteppedSliderAttribute(float min, float max, float step)
    {
        Min = min;
        Max = max;
        Step = step;
    }
}

