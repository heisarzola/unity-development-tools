

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Highlight Drawer.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Draws a given inspector area in a selected color when a condition is fulfilled (in the shape of a bool method, can be none).
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module: 
 *          -- None.
//----------------------------------------------------------------------*/

using System;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomPropertyDrawer(typeof(HighlightAttribute))]
public class HighlightDrawer : PropertyDrawer
{
    //------------------------------------------------------------------------------------//
    //----------------------------------- FIELDS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    private bool _alreadyAttemptedToRetrieve = false;
    private Type _classType = null;
    private MethodInfo _retrievedMethod = null;

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var highlightAttribute = attribute as HighlightAttribute;
        bool highlightTheProperty = true;

        // do we have a validation method 
        if (!string.IsNullOrEmpty(highlightAttribute.validateMethod))
        {
            // Cache the resource expensive reflection.
            if (!_alreadyAttemptedToRetrieve)
            {
                _alreadyAttemptedToRetrieve = true;
                _classType = property.serializedObject.targetObject.GetType();
                _retrievedMethod = _classType.GetMethod(highlightAttribute.validateMethod, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            }

            // Attempt to execute
            if (_retrievedMethod != null)
            {
                if (highlightAttribute.methodParameters.Length > 0)
                {
                    highlightTheProperty = (bool)_retrievedMethod.Invoke(property.serializedObject.targetObject, highlightAttribute.methodParameters);
                }
                else
                {

                    highlightTheProperty = (bool)_retrievedMethod.Invoke(property.serializedObject.targetObject, null);
                }
            }
            else
            {
                Debug.LogError(string.Format("An error occurred attempting to execute provided method: \"{0}\" on the object: \"{1}\". Please ensure right arguments were provided, the method name was typed correctly and that it returns a boolean.",
                    highlightAttribute.validateMethod, property.serializedObject.targetObject));
            }
        }


        if (highlightTheProperty)
        {
            // Draw the selected highlight color.
            float padding = EditorGUIUtility.standardVerticalSpacing;
            Rect highlightRect = new Rect(position.x - padding, position.y - padding, position.width + (padding * 2), position.height + (padding * 2));
            Color normalTextColor = EditorStyles.label.normal.textColor;

            EditorGUI.DrawRect(highlightRect, HighlightAttribute.GetConcreteColor(highlightAttribute.highlightColor));


            // Change label color
            EditorStyles.label.normal.textColor = HighlightAttribute.TextColorToUse(highlightAttribute.highlightColor);

            // Draw what is intended to show highlighted.
            EditorGUI.PropertyField(position, property, label);

            // Return label color back to normal.
            EditorStyles.label.normal.textColor = normalTextColor;
        }
        else // Won't be highlighted, draw normally.
        {
            EditorGUI.PropertyField(position, property, label);
        }

        // Force to always redraw, else when inspector gets out of focus, any highlight will also be turned off.
        EditorUtility.SetDirty(property.serializedObject.targetObject);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

}//End of class