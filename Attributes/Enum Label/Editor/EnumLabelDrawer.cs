

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Enum Label Drawer.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Replaces the display anme of an enum entry to a any chosen string.
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module: 
 *          -- None.
//----------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnumLabelAttribute))]
public class EnumLabelDrawer : PropertyDrawer
{
    //------------------------------------------------------------------------------------//
    //----------------------------------- FIELDS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    private readonly Dictionary<string, string> _customEnumNames = new Dictionary<string, string>();

    //------------------------------------------------------------------------------------//
    //--------------------------------- PROPERTIES ---------------------------------------//
    //------------------------------------------------------------------------------------//

    private EnumLabelAttribute EnumLabelAttr
    {
        get
        {
            return (EnumLabelAttribute)attribute;
        }
    }

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SetUpCustomEnumNames(property, property.enumNames);

        if (property.propertyType == SerializedPropertyType.Enum)
        {
            EditorGUI.BeginChangeCheck();
            string[] displayedOptions = property.enumNames
                .Where(enumName => _customEnumNames.ContainsKey(enumName))
                .Select<string, string>(enumName => _customEnumNames[enumName])
                .ToArray();
            int selectedIndex = EditorGUI.Popup(position, EnumLabelAttr.label, property.enumValueIndex, displayedOptions);
            if (EditorGUI.EndChangeCheck())
            {
                property.enumValueIndex = selectedIndex;
            }
        }
    }

    public void SetUpCustomEnumNames(SerializedProperty property, string[] enumNames)
    {
        Type type = property.serializedObject.targetObject.GetType();
        foreach (FieldInfo fieldInfo in type.GetFields())
        {
            object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(EnumLabelAttribute), false);
            foreach (EnumLabelAttribute customAttribute in customAttributes)
            {
                Type enumType = fieldInfo.FieldType;

                foreach (string enumName in enumNames)
                {
                    FieldInfo field = enumType.GetField(enumName);
                    if (field == null) continue;
                    EnumLabelAttribute[] attrs = (EnumLabelAttribute[])field.GetCustomAttributes(customAttribute.GetType(), false);

                    if (!_customEnumNames.ContainsKey(enumName))
                    {
                        foreach (EnumLabelAttribute labelAttribute in attrs)
                        {
                            _customEnumNames.Add(enumName, labelAttribute.label);
                        }
                    }
                }
            }
        }
    }
}//End of class