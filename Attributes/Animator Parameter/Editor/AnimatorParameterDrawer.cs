
/*---------------- Creation Date: 18-Jul-16 -----------------//
//------------ Last Modification Date: 18-Jul-16 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Animator Parameter Drawer.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Allows the selection of a valid parameter name from an animator to store it in the given string variable.
 *
 *   <<< LIMITATIONS >>>
 *       -- If you switch/delete/rename or move the order of a parameter that was referenced, issues might arise, including losing said references when viewed in the inspector.
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
 *       -- <<< EMPTY >>>
 *
 *   <<< SOURCES >>>
 *       -- [1] Drawer Idea : https://github.com/anchan828/property-drawer-collection/tree/master/AnimatorParameter
//---------------------------------------------------------------------*/

/*---------------------------- CHANGELOG -------------------------------//
 *   <<< V.1.0.0 -- 18-Jul-16 >>>
 *       -- Esentially copy/pasted the source code, the original developer did a good job. Pretty much only translated contents to english. [1]
//----------------------------------------------------------------------*/

using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

/// <summary>
/// <para>Custom drawer to list all the parameters of a mecanim <see cref="Animator"/>.</para>
/// </summary>
/// <exception cref='InvalidCastException'>
/// Is thrown when an explicit conversion (casting operation) fails because the source type cannot be converted to the destination type.
/// </exception>
/// <exception cref='MissingComponentException'>
/// Is thrown when the missing component exception.
/// </exception>
[CustomPropertyDrawer(typeof(AnimatorParameterAttribute))]
public class AnimatorParameterDrawer : PropertyDrawer // [1]
{
    //------------------------------------------------------------------------------------//
    //--------------------------------- PROPERTIES ---------------------------------------//
    //------------------------------------------------------------------------------------//

    AnimatorParameterAttribute AnimatorParameterAttr
    {
        get { return (AnimatorParameterAttribute)attribute; }
    }

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var animatorController = GetAnimatorController(property);

        if (animatorController == null)
        {
            DefaultInspector(position, property, label);
            return;
        }
        var parameters = animatorController.parameters;

        if (parameters.Length == 0)
        {
            Debug.LogWarning("The current \"Animator\" has no animation paramaters.");
            property.stringValue = string.Empty;
            DefaultInspector(position, property, label);
            return;
        }

        var eventNames = parameters
            .Where(t => CanAddEventName(t.type))
            .Select(t => t.name).ToList();

        if (eventNames.Count == 0)
        {
            Debug.LogWarning(string.Format("There aren't any parameters in the current \"Animator\" of the specified type ({0}).", AnimatorParameterAttr.parameterType));
            property.stringValue = string.Empty;
            DefaultInspector(position, property, label);
            return;
        }

        var eventNamesArray = eventNames.ToArray();

        var matchIndex = eventNames
            .FindIndex(eventName => eventName.Equals(property.stringValue));

        if (matchIndex != -1)
        {
            AnimatorParameterAttr.selectedValue = matchIndex;
        }

        AnimatorParameterAttr.selectedValue = EditorGUI.IntPopup(position, label.text,
            AnimatorParameterAttr.selectedValue, eventNamesArray, SetOptionValues(eventNamesArray));

        property.stringValue = eventNamesArray[AnimatorParameterAttr.selectedValue];

    }


    AnimatorController GetAnimatorController(SerializedProperty property)
    {
        var component = property.serializedObject.targetObject as Component;

        if (component == null)
        {
            throw new InvalidCastException("The current object has no components.");
        }

        var anim = component.GetComponent<Animator>();

        if (anim == null)
        {
            Debug.LogException(new MissingComponentException("Attempting to retrieve the animator parameters list of an object lacking the \"Animator\" component."));
            return null;
        }

        return anim.runtimeAnimatorController as AnimatorController;
    }

    /// <summary>
    /// Determines whether this instance can add event name the specified animatorController index.
    /// </summary>
    bool CanAddEventName(AnimatorControllerParameterType animatorControllerParameterType)
    {
        return !(AnimatorParameterAttr.parameterType != AnimatorParameterAttribute.ParameterType.None
                 && (int)animatorControllerParameterType != (int)AnimatorParameterAttr.parameterType);
    }

    /// <summary>
    /// Sets the option values.
    /// </summary>
    int[] SetOptionValues(string[] eventNames)
    {
        int[] optionValues = new int[eventNames.Length];
        for (int i = 0; i < eventNames.Length; i++)
        {
            optionValues[i] = i;
        }
        return optionValues;
    }

    void DefaultInspector(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label);
    }
} //End of class