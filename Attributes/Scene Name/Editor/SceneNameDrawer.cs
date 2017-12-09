
/*---------------- Creation Date: 18-Jul-16 -----------------//
//------------ Last Modification Date: 18-Jul-16 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Read Only Attribute.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Empty attribute intended to leave disabled any provided field it is found above. (This functionality comes from its drawer)
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
 *       -- [1] Drawer Idea : http://www.jeffpizano.com/blog/2015/06/read-only-properties-in-unity3d-inspector/
//---------------------------------------------------------------------*/

/*---------------------------- CHANGELOG -------------------------------//
 *   <<< V.1.0.0 -- 18-Jul-16 >>>
 *       -- Created using the given reference. [1]
//----------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SceneNameAttribute))]
public class SceneNameDrawer : PropertyDrawer
{
    //------------------------------------------------------------------------------------//
    //--------------------------------- PROPERTIES ---------------------------------------//
    //------------------------------------------------------------------------------------//

    private SceneNameAttribute SceneNameAttribute
    {
        get
        {
            return (SceneNameAttribute)attribute;
        }
    }

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string[] sceneNames = GetEnabledSceneNames();

        if (sceneNames.Length == 0)
        {
            EditorGUI.LabelField(position, ObjectNames.NicifyVariableName(property.name), "Scene is Empty");
            return;
        }

        int[] sceneNumbers = new int[sceneNames.Length];

        SetSceneNumbers(sceneNumbers, sceneNames);

        if (!string.IsNullOrEmpty(property.stringValue))
            SceneNameAttribute.selectedValue = GetIndex(sceneNames, property.stringValue);

        SceneNameAttribute.selectedValue = EditorGUI.IntPopup(position, label.text, SceneNameAttribute.selectedValue, sceneNames, sceneNumbers);

        property.stringValue = sceneNames[SceneNameAttribute.selectedValue];
    }

    string[] GetEnabledSceneNames()
    {
        List<EditorBuildSettingsScene> scenes = (SceneNameAttribute.activeScenesOnly ? EditorBuildSettings.scenes.Where(scene => scene.enabled) : EditorBuildSettings.scenes).ToList();
        HashSet<string> sceneNames = new HashSet<string>();
        scenes.ForEach(scene =>
        {
            sceneNames.Add(scene.path.Substring(scene.path.LastIndexOf("/", StringComparison.Ordinal) + 1).Replace(".unity", string.Empty));
        });
        return sceneNames.ToArray();
    }

    void SetSceneNumbers(int[] sceneNumbers, string[] sceneNames)
    {
        for (int i = 0; i < sceneNames.Length; i++)
        {
            sceneNumbers[i] = i;
        }
    }

    int GetIndex(string[] sceneNames, string sceneName)
    {
        int result = 0;
        for (int i = 0; i < sceneNames.Length; i++)
        {
            if (sceneName == sceneNames[i])
            {
                result = i;
                break;
            }
        }
        return result;
    }
}