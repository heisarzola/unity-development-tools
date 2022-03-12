

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Read Only Attribute.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Empty attribute intended to leave disabled any provided field it is found above. (This functionality comes from its drawer)
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module: 
 *          -- None.
//----------------------------------------------------------------------*/

using UnityEngine;
using UnityEditor;

///<summary>A class to make Read-Only inspector properties.</summary>
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer // [1]
{
    ///<summary>Necessary since some properties tend to collapse smaller than their content.</summary>
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }//End of GetPropertyHeight


    ///<summary>Draw a disabled property field.</summary>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false; // Disable fields
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true; // Enable fields
    }//End of OnGUI
    
}//End of ReadOnlyDrawer