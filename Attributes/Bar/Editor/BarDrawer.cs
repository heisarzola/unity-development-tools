
/*---------------- Creation Date: 09-Dec-17 -----------------//
//------------ Last Modification Date: 09-Dec-17 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Bar Attribute.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Draws a bar on top of the specificed numerical value, filled based on a specified max bar value.
 *
 *   <<< LIMITATIONS >>>
 *       -- None.
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module: 
 *          -- None.
//----------------------------------------------------------------------*/

/*------------------------------- NOTES --------------------------------//
 *   <<< TO-DO LIST >>>
 *       -- <<< EMPTY >>>
 *
 *   <<< POSSIBLES >>>
 *       -- Include the possibility of drawing from a property, not only int and float fields.
 *
 *   <<< SOURCES >>>
 *       -- [1] Base Class : https://gist.github.com/LotteMakesStuff/2d3c6dc7a913ed118601db95735574de
//---------------------------------------------------------------------*/

/*---------------------------- CHANGELOG -------------------------------//
 *   <<< V.1.0.0 -- 09-Dec-17 >>>
 *       -- Created using the reference. [1]
 *       -- Improved by caching the expensive reflection operations. 
 *       -- Improved by ensuring the colored always remained visible.
 *       -- Improved by adding several new color options.
 *       -- Added ability to provide a custom initial value, and support properties and parameterless methods.
 *       -- Improved by allowing any value (floats were allowed only when less than 1).
//----------------------------------------------------------------------*/

using System;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomPropertyDrawer(typeof(BarAttribute))]
public class BarDrawer : PropertyDrawer
{
    //------------------------------------------------------------------------------------//
    //----------------------------- ENUM DECLARATIONS ------------------------------------//
    //------------------------------------------------------------------------------------//

    private enum MaxValueUsed
    {
        Field,
        Property,
        Method,
        StaticValue
    }

    //------------------------------------------------------------------------------------//
    //----------------------------------- FIELDS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    private SerializedProperty _retrievedField;
    private Type _classType = null;
    private PropertyInfo _retrievedProperty = null;
    private MethodInfo _retrievedMethod = null;
    private bool _alreadyAttemptedToRetrieve = false;

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    private float GetMax(MaxValueUsed maxValue, SerializedProperty property, float defaultValue)
    {
        switch (maxValue)
        {
            case MaxValueUsed.Field:
                return _retrievedField.propertyType == SerializedPropertyType.Float
                    ? _retrievedField.floatValue
                    : _retrievedField.intValue;
            case MaxValueUsed.Method:
                return (float)_retrievedMethod.Invoke(property.serializedObject.targetObject, null);
            case MaxValueUsed.Property:
                return (float)_retrievedProperty.GetAccessors()[0].Invoke(property.serializedObject.targetObject, null);
            default:
                return defaultValue;
        }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        #region Variable Declaration

        var barAttribute = attribute as BarAttribute;

        float lineHight = EditorGUIUtility.singleLineHeight;
        float padding = EditorGUIUtility.standardVerticalSpacing;

        var barPosition = new Rect(position.position.x, position.position.y, position.size.x, lineHight);

        float fillPercentage = 0;
        string barLabel = string.Empty;
        bool maxValueIsInvalid = false;
        bool currentFieldIsInvalid = false;
        bool error = false;

        #endregion Variable Declaration

        #region Cache Reflection

        if (!_alreadyAttemptedToRetrieve)
        {
            _alreadyAttemptedToRetrieve = true;

            _classType = property.serializedObject.targetObject.GetType();
            _retrievedField = property.serializedObject.FindProperty(barAttribute.maxValueName);

            if (barAttribute.maxValueName != null)
            {
                _retrievedProperty = _classType.GetProperty(barAttribute.maxValueName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                _retrievedMethod = _classType.GetMethod(barAttribute.maxValueName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            }
        }

        #endregion Cache Reflection

        MaxValueUsed maxValueType = MaxValueUsed.StaticValue;

        if (barAttribute.UsingStaticMaxValue)
        {
            maxValueType = MaxValueUsed.StaticValue;
        }
        else if (_retrievedMethod != null)
        {
            maxValueType = MaxValueUsed.Method;
        }
        else if (_retrievedProperty != null)
        {
            maxValueType = MaxValueUsed.Property;
        }
        else if (_retrievedField != null)
        {
            maxValueType = MaxValueUsed.Field;
        }
        else // Provided Max Value Is Invalid
        {
            maxValueIsInvalid = true;
            barLabel = "Invalid Field/Property/Method. Please Check.";
        }

        // If field with parameter is invalid.
        currentFieldIsInvalid = property.propertyType != SerializedPropertyType.Integer &&
                                property.propertyType != SerializedPropertyType.Float;

        if (currentFieldIsInvalid)
        {
            string message = string.Format("The <b><i>BarAttribute</i></b> Cannot Be Used On \"<i><b>{0}s</b></i>\". Only Ints and Floats.", property.propertyType.ToString());
            barLabel = barLabel == String.Empty ? message : string.Format("{0}\nAlso, {1}", barLabel, message);
        }

        error = currentFieldIsInvalid || maxValueIsInvalid;


        // No errors, get normal bar label.
        if (!error)
        {
            float current = property.propertyType == SerializedPropertyType.Integer
                ? property.intValue
                : property.floatValue;

            float max = GetMax(maxValueType, property, barAttribute.staticMaxValue);

            // Get Fill %
            fillPercentage = current / max;

            // Draw Label Text
            barLabel = string.Format("{0} ({3:0.0}%) | {1:0.0}/{2:0.0}",
                string.Format("{0}{1}", property.name[0].ToString().ToUpperInvariant(), property.name.Substring(1)),
                current,
                max,
                fillPercentage * 100);
        }

        EditorGUI.PropertyField(new Rect(position.position.x, position.position.y + lineHight + padding, position.size.x, lineHight), property);

        #region Draw Result

        if (error)
        {
            GUI.Label(barPosition, string.Format("<color=red>{0}</color>", barLabel), new GUIStyle { richText = true });
        }
        else
        {
            var color = BarAttribute.GetConcreteColor(barAttribute.color);
            var color2 = Color.white;
            DrawBar(barPosition, Mathf.Clamp01(fillPercentage), barLabel, color, color2);
        }

        #endregion Draw Result

        // Force to always redraw, else when inspector gets out of focus, any highlight will also be turned off.
        EditorUtility.SetDirty(property.serializedObject.targetObject);
    }

    private void DrawBar(Rect position, float fillPercent, string label, Color barColor, Color labelColor)
    {
        if (Event.current.type != EventType.Repaint)
            return;

        Color savedColor = GUI.color;

        var fillRect = new Rect(position.x, position.y, position.width * fillPercent, position.height);

        EditorGUI.DrawRect(position, new Color(0.1f, 0.1f, 0.1f));
        EditorGUI.DrawRect(fillRect, barColor);

        // set alignment and cache the default
        var align = GUI.skin.label.alignment;
        GUI.skin.label.alignment = TextAnchor.UpperCenter;

        // set the color and cache the default
        var c = GUI.contentColor;
        GUI.contentColor = labelColor;

        // calculate the position
        var labelRect = new Rect(position.x, position.y - 3, position.width, position.height);

        // draw~
        EditorGUI.DropShadowLabel(labelRect, label);

        // reset color and alignment
        GUI.contentColor = c;
        GUI.skin.label.alignment = align;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return (EditorGUIUtility.singleLineHeight * 2) + EditorGUIUtility.standardVerticalSpacing;
    }

}