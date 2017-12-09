
/*---------------- Creation Date: 19-Jan-17 -----------------//
//------------ Last Modification Date: 19-Jan-17 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomPropertyDrawer(typeof(InspectorMethodAttribute))]
public class InspectorMethodDrawer : PropertyDrawer
{
    //------------------------------------------------------------------------------------//
    //----------------------------------- FIELDS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    private InspectorMethodAttribute _methodAttr;
    private Object _obj;
    private Rect _buttonRect;
    private Rect _valueRect;

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        _methodAttr = attribute as InspectorMethodAttribute;
        _obj = property.serializedObject.targetObject;
        MethodInfo method = _obj.GetType().GetMethod(_methodAttr.methodName, _methodAttr.flags);

        if (method == null) // Requested method DOESN'T exist?
        {
            EditorGUI.HelpBox(position, "Method Name Not Found", MessageType.Error);
        }
        else
        {// Method Found
            if (_methodAttr.useValue)
            {
                _valueRect = new Rect(position.x, position.y, position.width / 2f, position.height);
                _buttonRect = new Rect(position.x + position.width / 2f, position.y, position.width / 2f, position.height);

                EditorGUI.PropertyField(_valueRect, property, GUIContent.none);
                if (GUI.Button(_buttonRect, _methodAttr.buttonName))
                {
                    method.Invoke(_obj, new object[] { fieldInfo.GetValue(_obj) });
                }

            }
            else
            {
                if (GUI.Button(position, _methodAttr.buttonName))
                {
                    method.Invoke(_obj, null);
                }
            }
        }//End of Method Found

    }//End of OnGUI(Rect position, SerializedProperty property, GUIContent label)

}//End of class