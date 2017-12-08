
/*---------------- Creation Date: 13-Jul-16 -----------------//
//------------ Last Modification Date: 13-Jul-16 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Ranged Int Drawer.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Fallback drawer in case the class RangedInt is inteneded to be used without the RangedIntAttribute.
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
 *       -- <<< EMPTY >>>
 *
 *   <<< SOURCES >>>
 *       -- <<< EMPTY >>>
//---------------------------------------------------------------------*/

/*---------------------------- CHANGELOG -------------------------------//
 *   <<< V.1.0.0 -- 13-Jul-16 >>>
 *       -- Class creation.
//----------------------------------------------------------------------*/

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RangedInt))]
public class RangedIntDrawer : PropertyDrawer
{
    //------------------------------------------------------------------------------------//
    //----------------------------------- FIELDS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    private const int _AMOUNT_OF_ITEMS = 1;
    private readonly float _spacerHeight = 20f;
    private readonly float _lineHeight = 16f;
    private string _name = string.Empty;
    private string _tooltip = string.Empty;
    private bool _cache = false;

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return _AMOUNT_OF_ITEMS * _spacerHeight;
    }//End of GetPropertyHeight(SerializedProperty property, GUIContent label)

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUIUtility.labelWidth /= 4f;
        position.height = _lineHeight;
        position.width /= 4f;

        if (!_cache)
        {
            //get the name before it's gone
            _name = property.displayName;
            _tooltip = property.tooltip;

            _cache = true;
        }

        EditorGUI.PrefixLabel(position, new GUIContent(_name,
            string.Format("Base Tooltip: {0}", _tooltip.Equals(string.Empty) ? "" : string.Format("\n\n{0}'s Tooltip:\n{1}", _name, _tooltip))));

        position.x += position.width;

        position.width *= 4f;
        position.width *= 0.375f;

        EditorGUI.PropertyField(position, property.FindPropertyRelative("min"),
            new GUIContent("Min"));

        position.x += position.width;

        EditorGUI.PropertyField(position, property.FindPropertyRelative("max"),
            new GUIContent("Max"));

    }//End of OnGUI()

}//End of class