

/*---------------- Creation Date: 15-Dec-17 -----------------//
//------------ Last Modification Date: 15-Dec-17 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Auto Saver.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Class in charge of autosaving scenes after a set interval of time, before entering playmode and in other relevant moments.
 *
 *   <<< LIMITATIONS >>>
 *       -- None.
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module: None.
//----------------------------------------------------------------------*/

/*------------------------------- NOTES --------------------------------//
 *   <<< TO-DO LIST >>>
 *       -- <<< EMPTY >>>
 *
 *   <<< POSSIBLES >>>
 *       -- <<< EMPTY >>>
 *
 *   <<< SOURCES >>>
 *       -- [1] Base class, saves every 5 minutes : https://github.com/liortal53/AutoSaveScene/blob/master/Assets/Editor/AutoSaveScene.cs
//---------------------------------------------------------------------*/

/*---------------------------- CHANGELOG -------------------------------//
 *   <<< V.1.0.0 -- 15-Dec-17 >>>
 *       -- Class created, supports auto save every 5 minutes, when playmode starts, it also attempts to save before editor closes.
//----------------------------------------------------------------------*/

using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class AutoSaver
{
    //------------------------------------------------------------------------------------//
    //----------------------------------- FIELDS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    private static System.DateTime _lastSaveTime = System.DateTime.Now;
    private static readonly System.TimeSpan _updateInterval;

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    static AutoSaver()
    {
        EditorApplication.playModeStateChanged += SaveScene;
        _updateInterval = new TimeSpan(0, 5, 0);
        EditorApplication.update += OnUpdate;
    }

    static void SaveScene(PlayModeStateChange playModeStateChange)
    {
        if (playModeStateChange == PlayModeStateChange.ExitingEditMode)
        {
            Debug.Log("Auto-Saving scene before entering Play mode: ".Append(SceneManager.GetActiveScene().name));
            SaveScene();
        }
    }

    private static void SaveScene()
    {
        EditorSceneManager.SaveOpenScenes();
    }

    private static void OnUpdate()
    {
        if ((System.DateTime.Now - _lastSaveTime) >= _updateInterval)
        {
            SaveScene();
            _lastSaveTime = System.DateTime.Now;
        }
    }
}