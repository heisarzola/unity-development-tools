﻿

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Ranged Float Attribute Drawer.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Class that draws the ranged float attribute.
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module: 
 *          -- None.
//----------------------------------------------------------------------*/

using System.Globalization;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(RangedIntAttribute))]
class RangedIntAttributeDrawer : PropertyDrawer // [1]
{
    private const float _COMPONENT_HEIGHT = 16f;
    private const float _VERTICAL_PADDING = 2f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        if (property.type.Equals(typeof(RangedInt).Name))
        {

            #region Variable Initialization

            SerializedProperty currentMin = property.FindPropertyRelative("min");
            SerializedProperty currentMax = property.FindPropertyRelative("max");

            float currentMaxCopy = currentMax.intValue;
            float currentMinCopy = currentMin.intValue;

            RangedIntAttribute validRange = attribute as RangedIntAttribute;

            #endregion Variable Initialization

            #region Editor Drawing

            label = EditorGUI.BeginProperty(position, label, property);

            Rect sliderRect = new Rect(position.x,
                                       position.y,
                                       position.width,
                                       _COMPONENT_HEIGHT);

            EditorGUI.BeginChangeCheck();

            // Given how the valid ranges themselves aren't stored anywhere, when you close the inspector the valid range values will reset.
            // In an attempt to "remember" them, if the current value "breaks" the limits, it means that the limit was lowered, or increased manually last time, so the current values will be used as temporal limits when reloaded.
            if (validRange.rangeDisplayType == RangedIntAttribute.RangeDisplayType.EditableRanges)
            {
                if (validRange.min > currentMinCopy)
                {
                    validRange.min = (int)currentMinCopy;
                }

                if (validRange.max < currentMaxCopy)
                {
                    validRange.max = (int)currentMaxCopy;
                }
            }

            EditorGUI.MinMaxSlider(sliderRect, label, ref currentMinCopy, ref currentMaxCopy, validRange.min, validRange.max);

            if (validRange.rangeDisplayType != RangedIntAttribute.RangeDisplayType.HideRanges)
            {
                Rect lower = EditorGUI.PrefixLabel(sliderRect, label);
                lower.y += _COMPONENT_HEIGHT + _VERTICAL_PADDING;
                Rect upper = new Rect(lower.x,
                    lower.y + _COMPONENT_HEIGHT + _VERTICAL_PADDING,
                    lower.width,
                    _COMPONENT_HEIGHT);

                if (validRange.rangeDisplayType == RangedIntAttribute.RangeDisplayType.LockedRanges)
                {
                    currentMinCopy = EditorGUI.FloatField(lower, string.Format("Lower (Min: {0})", validRange.min.ToString(CultureInfo.InvariantCulture)), currentMinCopy);
                    currentMaxCopy = EditorGUI.FloatField(upper, string.Format("Upper (Max: {0})", validRange.max.ToString(CultureInfo.InvariantCulture)), currentMaxCopy);
                }
                else if (validRange.rangeDisplayType == RangedIntAttribute.RangeDisplayType.EditableRanges)
                {
                    // Draw lower
                    lower.width /= 4f;
                    EditorGUI.LabelField(lower, new GUIContent("Lowest", "Minimal value that the lower bound can get to."));
                    lower.x += lower.width;
                    validRange.min = EditorGUI.IntField(lower, validRange.min);
                    lower.x += lower.width;
                    EditorGUI.LabelField(lower, new GUIContent("Current", "The current min value in the slider."));
                    lower.x += lower.width;
                    currentMinCopy = EditorGUI.FloatField(lower, currentMinCopy);

                    // Draw upper
                    upper.width /= 4f;
                    EditorGUI.LabelField(upper, new GUIContent("Highest", "Maximum value that the upper bound can get to."));
                    upper.x += upper.width;
                    validRange.max = EditorGUI.IntField(upper, validRange.max);
                    upper.x += upper.width;
                    EditorGUI.LabelField(upper, new GUIContent("Current", "The current max value in the slider."));
                    upper.x += upper.width;
                    currentMaxCopy = EditorGUI.FloatField(upper, currentMaxCopy);
                }
            }



            #endregion Editor Drawing

            #region Clamp Values

            if (EditorGUI.EndChangeCheck())
            {
                // If it is attempted to make upper limit smaller than lower, clamp the upper limit to the value of the lower.
                if (currentMaxCopy < currentMinCopy) { currentMax.intValue = currentMin.intValue; }
                // Is the provided lower limit valid? If so, keep it.
                else if (currentMinCopy < validRange.min) { currentMin.intValue = validRange.min; }
                // The provided lower value is smaller than the lower limit, clamp to minimal accepted.
                else { currentMin.intValue = (int)currentMinCopy; }

                // If it is attempted to make lower limit greater than upper, clamp the lower limit to the value of the upper.
                if (currentMinCopy > currentMaxCopy) { currentMin.intValue = currentMax.intValue; }
                // Is the provided upper value valid? If so, keep it.
                else if (currentMaxCopy > validRange.max) { currentMax.intValue = validRange.max; }
                // The provided max value is greater than the allowed max, clamp it to max allowed.
                else { currentMax.intValue = (int)currentMaxCopy; }

            }
            #endregion Clamp Values

            EditorGUI.EndProperty();

        }
        else
        {
            Debug.LogError(string.Format("Attempting to use the <b>'[{0}(int min, int max)]'</b> attribute on a <color=red>'{1}'</color> type field. Should be <color=green>'{2}'</color> instead.",
                typeof(RangedInt).Name, property.type, typeof(RangedInt).Name));
        }
    }//End of OnGUI


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        RangedIntAttribute rangedIntAttribute = attribute as RangedIntAttribute;
        int additionalRows = rangedIntAttribute.rangeDisplayType == RangedIntAttribute.RangeDisplayType.HideRanges ? 0 : 2;
        return base.GetPropertyHeight(property, label) + _COMPONENT_HEIGHT + (additionalRows * _COMPONENT_HEIGHT);
    }//End of GetPropertyHeight(SerializedProperty property, GUIContent label)

}//End of class