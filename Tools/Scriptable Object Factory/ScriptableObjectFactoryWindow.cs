
/*---------------- Creation Date: 15-Apr-17 -----------------//
//------------ Last Modification Date: 15-Jun-17 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Scriptable Object Factory Window.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Window that displays a list of all scriptable objects in the project to easily create any as needed.
 *
 *   <<< LIMITATIONS >>>
 *       -- Only creates one scriptable object at a time.
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None
 *       -- Module: ScriptableObjectFactory
//----------------------------------------------------------------------*/

/*------------------------------- NOTES --------------------------------//
 *   <<< TO-DO LIST >>>
 *       -- <<< EMPTY >>>
 *
 *   <<< POSSIBLES >>>
 *       -- Add support for filtering via a textbox.
 *
 *   <<< SOURCES >>>
 *       -- [1] Almost all main class : https://github.com/liortal53/ScriptableObjectFactory/blob/master/Assets/Editor/ScriptableObjectWindow.
//---------------------------------------------------------------------*/

/*---------------------------- CHANGELOG -------------------------------//
 *   <<< V.1.0.0 -- 15-Apr-17 >>>
 *       -- Created base class according to source.
 *   <<< V.1.0.1 -- 15-Apr-17 >>>
 *       -- Cleaned the name of the generated asset by removing namespaces, implemented neat naming convention.
 *   <<< V.1.0.2 -- 23-May-17 >>>
 *       -- Made the window dockable and allowed it to restart its values on recompile. Added alphabetic ordering.
 *   <<< V.1.0.3 -- 15-Jun-17 >>>
 *       -- Fixed a bug where setting the namespace option off didn't alphabetically reorganize the SO list, and adapted it to work with the "first-pass" assembly (Plugins, etc).
//----------------------------------------------------------------------*/

using System;
using System.Linq;
using System.Reflection;
using System.Text;
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

/// <summary>
/// Window that displays a list of all scriptable objects in the project to easily create any as needed.
/// </summary>
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

    /// <summary>
    /// Initialize window.
    /// </summary>
    public static void Init(Type[] scriptableObjects)
    {
        Types = scriptableObjects;

        var window = EditorWindow.GetWindow<ScriptableObjectFactoryWindow>(false, "Scriptable Object Factory", true);
        window.ShowPopup();
    }

    /// <summary>
    /// Re-Fetches and redraws classes on compile, change, and other events.
    /// </summary>
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

        Types = ScriptableObjectFactory.ConcatArrays(allScriptableObjects, allPluginScriptableObjects).OrderBy(type => (_useNamespaces ? type.FullName : type.Name)).ToArray();
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

    /// <summary>
    /// Separate a class name into a neatly organized string.
    /// </summary>
    private static string NormalizeScriptableObjectName(string className)
    {
        StringBuilder result = new StringBuilder();

        foreach (char c in className)
        {
            if (char.IsUpper(c))
            {
                result.Append(" ");
            }
            result.Append(c.ToString());
        }

        return result.ToString().Substring(1);
    }

}//End of class
