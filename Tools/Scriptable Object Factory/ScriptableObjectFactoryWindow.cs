
/*---------------- Creation Date: 15-Apr-17 -----------------//
//------------ Last Modification Date: 15-Jun-17 ------------//
//------ Luis Raul Arzola Lopez : http://HeIsArzola.com ------*/

using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

//------------------------------------------------------------------------------------//
//--------------------------- CLASSES DECLARATIONS -----------------------------------//
//------------------------------------------------------------------------------------//

internal class EndNameEdit : EndNameEditAction
{
    #region implemented abstract members of EndNameEditAction

    public override void Action(int instanceId, string pathName, string resourceFile)
    {
        AssetDatabase.CreateAsset(EditorUtility.InstanceIDToObject(instanceId),
            AssetDatabase.GenerateUniqueAssetPath(pathName));
    }

    #endregion
}//End of EndNameEditAction

public class ScriptableObjectFactoryWindow : EditorWindow // [1]
{
    //------------------------------------------------------------------------------------//
    //----------------------------------- FIELDS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    private const string SCRIPTABLE_OBJECT_FACTORY_USES_NAMESPACES = "SCRIPTABLE_OBJECT_FACTORY_USES_NAMESPACES";

    private static int _selectedIndex = 0;
    private static string[] _names;
    private static string[] _fullnames;
    private static bool _useNamespaces = true;
    private static Type[] _types;

    //------------------------------------------------------------------------------------//
    //--------------------------------- PROPERTIES ---------------------------------------//
    //------------------------------------------------------------------------------------//

    private static Type[] Types
    {
        get { return _types; }
        set
        {
            _types = value;
            _names = _types.Select(t => t.Name).ToArray();
            _fullnames = _types.Select(t => t.FullName).ToArray();
        }
    }

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    private static string NormalizeScriptableObjectName(string scriptableObjectClassName)
    {
        return scriptableObjectClassName.FromCamelCaseToSeparated(true);
    }

    public static void Init(Type[] scriptableObjects)
    {
        Types = scriptableObjects;

        var window = EditorWindow.GetWindow<ScriptableObjectFactoryWindow>(false, "Scriptable Object Factory", true);
        window.ShowPopup();
    }

    public static void Restart()
    {
        var allScriptableObjects = (from t in ScriptableObjectFactory.GetAssembly().GetTypes()
                                    where t.IsSubclassOf(typeof(ScriptableObject))
                                    where !t.Name.Contains(typeof(EndNameEdit).ToString())
                                    where !t.IsSubclassOf(typeof(EditorWindow))
                                    where !t.IsAbstract
                                    where !t.IsGenericType
                                    select t).ToArray();

        Type[] allPluginScriptableObjects = new Type[0];

        Assembly pluginAssembly = ScriptableObjectFactory.GetPluginAssembly();

        if (pluginAssembly != null)
        {
            allPluginScriptableObjects = (from t in ScriptableObjectFactory.GetPluginAssembly().GetTypes()
                                          where t.IsSubclassOf(typeof(ScriptableObject))
                                          where !t.Name.Contains(typeof(EndNameEdit).ToString())
                                          where !t.IsSubclassOf(typeof(EditorWindow))
                                          where !t.Name.Contains("EndNameEdit")
                                          where !t.IsAbstract
                                          where !t.IsGenericType
                                          select t).OrderBy(s => s.Name).ToArray();
        }

        Types = allScriptableObjects.ConcatArrays(allPluginScriptableObjects).OrderBy(type => (_useNamespaces ? type.FullName : type.Name)).ToArray();
    }

    public void OnGUI()
    {
        GUILayout.Label("All Scriptable Objects in Project");

        EditorGUILayout.Space();

        if (Types == null)
        {
            Restart();
        }

        _selectedIndex = (_useNamespaces ? EditorGUILayout.Popup(_selectedIndex, _fullnames) : EditorGUILayout.Popup(_selectedIndex, _names));

        if (GUILayout.Button("Create"))
        {
            var asset = ScriptableObject.CreateInstance(_types[_selectedIndex]);
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                asset.GetInstanceID(),
                ScriptableObject.CreateInstance<EndNameEdit>(),
                string.Format("{0}.asset", "AssetNameHere - (" + NormalizeScriptableObjectName(_names[_selectedIndex]) + ")"),
                AssetPreview.GetMiniThumbnail(asset),
                null);
        }

        EditorGUILayout.Space();

        EditorGUI.BeginChangeCheck();

        _useNamespaces = PlayerPrefs.GetInt(SCRIPTABLE_OBJECT_FACTORY_USES_NAMESPACES, 1) == 1;
        bool tmpUsesNamespaces = _useNamespaces;

        _useNamespaces = EditorGUILayout.Toggle(new GUIContent("Show Namespaces",
            "Should the scriptable object list show namespaces?\n\nFor visual-aid purposes only. Created assets won't have the namespace."),
            _useNamespaces);


        if (tmpUsesNamespaces != _useNamespaces)
        {
            PlayerPrefs.SetInt(SCRIPTABLE_OBJECT_FACTORY_USES_NAMESPACES, _useNamespaces ? 1 : 0);
            PlayerPrefs.Save();
        }

        if (EditorGUI.EndChangeCheck())
        {
            // Do something when the property changes 
            Repaint();
            Restart();
        }
    }

}//End of class

public static class StringExtension
{
    public static string FromCamelCaseToSeparated(this string thisString, bool ignoreFirstSpace = false)
    {
        string result = "";

        foreach (char c in thisString)
        {
            if (char.IsUpper(c))
                result += " ";
            result += c.ToString();
        }

        // Delete first space
        if (ignoreFirstSpace)
        {
            result = result.Substring(1);
        }

        return result;
    }
}