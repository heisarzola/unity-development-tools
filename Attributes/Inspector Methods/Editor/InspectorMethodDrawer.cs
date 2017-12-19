
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

    private Object _obj;
    private MethodInfo _method;
    private bool _hasBeenInitialized = false;

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        InspectorMethodAttribute methodAttr = attribute as InspectorMethodAttribute;
        if (!_hasBeenInitialized)
        {
            _obj = property.serializedObject.targetObject;
            _method = _obj.GetType().GetMethod(methodAttr.methodName, methodAttr.flags);
        }

        if (_method == null) // Requested method DOESN'T exist?
        {
            EditorGUI.HelpBox(position, "Method Name Not Found", MessageType.Error);
        }
        else
        {// Method Found
            if (methodAttr.useValue)
            {
                Rect valueRect = new Rect(position.x, position.y, position.width / 2f, position.height);
                Rect buttonRect = new Rect(position.x + position.width / 2f, position.y, position.width / 2f, position.height);

                EditorGUI.PropertyField(valueRect, property, GUIContent.none);
                if (GUI.Button(buttonRect, methodAttr.buttonName))
                {
                    _method.Invoke(_obj, new object[] { fieldInfo.GetValue(_obj) });
                }

            }
            else
            {
                if (GUI.Button(position, methodAttr.buttonName))
                {
                    _method.Invoke(_obj, null);
                }
            }
        }//End of Method Found

    }//End of OnGUI(Rect position, SerializedProperty property, GUIContent label)

}//End of class