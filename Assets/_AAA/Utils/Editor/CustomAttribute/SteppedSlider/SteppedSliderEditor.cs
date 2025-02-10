
#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SteppedSliderAttribute))]
public class SteppedSliderDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SteppedSliderAttribute steppedSlider = (SteppedSliderAttribute)attribute;

        float value = property.floatValue;
        float stepValue = Mathf.Round(value / steppedSlider.Step) * steppedSlider.Step;

        float newValue = EditorGUI.Slider(position, label, stepValue, steppedSlider.Min, steppedSlider.Max);
        newValue = Mathf.Round(newValue / steppedSlider.Step) * steppedSlider.Step; // Apply stepping
        newValue = Mathf.Clamp(newValue, steppedSlider.Min, steppedSlider.Max);

        if (newValue != value)
        {
            property.floatValue = newValue;
        }
    }
}
#endif