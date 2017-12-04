
/*---------------- Creation Date: 19-Jan-17 -----------------//
//------------ Last Modification Date: 19-Jan-17 ------------//
//------ Luis Raul Arzola Lopez : http://heisarzola.com ------*/

using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomPropertyDrawer(typeof(InspectorMethodAttribute))]
public class InspectorMethodDrawer : PropertyDrawer
{
    #region Private Variables


    InspectorMethodAttribute methodAttr;
    Object obj;
    Rect buttonRect;
    Rect valueRect;


    #endregion Private Variables


    #region Public Method


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        methodAttr = attribute as InspectorMethodAttribute;
        obj = property.serializedObject.targetObject;
        MethodInfo method = obj.GetType().GetMethod(methodAttr.methodName, methodAttr.flags);

        if (method == null) // Requested method DOESN'T exist?
        {
            EditorGUI.HelpBox(position, "Method Name Not Found", MessageType.Error);
        }
        else
        {// Method Found
            if (methodAttr.useValue)
            {
                valueRect = new Rect(position.x, position.y, position.width / 2f, position.height);
                buttonRect = new Rect(position.x + position.width / 2f, position.y, position.width / 2f, position.height);

                EditorGUI.PropertyField(valueRect, property, GUIContent.none);
                if (GUI.Button(buttonRect, methodAttr.buttonName))
                {
                    method.Invoke(obj, new object[] { fieldInfo.GetValue(obj) });
                }

            }
            else
            {
                if (GUI.Button(position, methodAttr.buttonName))
                {
                    method.Invoke(obj, null);
                }
            }
        }//End of Method Found

    }//End of OnGUI(Rect position, SerializedProperty property, GUIContent label)


    #endregion Public Method


}//End of class