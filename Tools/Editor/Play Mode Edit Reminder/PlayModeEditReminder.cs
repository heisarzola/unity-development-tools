
/*---------------- Creation Date: 15-Dec-17 -----------------//
//------------ Last Modification Date: 15-Dec-17 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Play Mode Edit Reminder.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Class in charge of reminding a user that he/she is attempting to make changes to scene while in play mode.
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
 *       -- [1] Unedited Original Class : Lost original reference URL ;\
//---------------------------------------------------------------------*/

/*---------------------------- CHANGELOG -------------------------------//
 *   <<< V.1.0.0 -- 15-Dec-17 >>>
 *       -- Made class based on reference [1].
 *       -- Improved by caching the GUIStyle used.
//----------------------------------------------------------------------*/

using UnityEngine;
using UnityEditor;
[InitializeOnLoad, ExecuteInEditMode]
public static class PlayModeEditReminder
{
    //------------------------------------------------------------------------------------//
    //----------------------------------- FIELDS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    private static GUIStyle _style;

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    static PlayModeEditReminder()
    {
        SceneView.onSceneGUIDelegate += OnSceneGUI;
    }
    static void OnSceneGUI(SceneView view)
    {
        if (Application.isPlaying && Selection.activeGameObject != null && Selection.activeGameObject.scene.name != null)
        {
            Handles.BeginGUI();
            if (_style == null)
            {
                _style = new GUIStyle(GUI.skin.GetStyle("label"))
                {
                    fontSize = 20,
                    contentOffset = Vector2.zero,
                    overflow = new RectOffset(),
                    margin = new RectOffset(0, 0, 0, 40),
                    alignment = TextAnchor.LowerCenter,
                    fontStyle = FontStyle.Bold,
                    wordWrap = true,
                    stretchHeight = true
                };
            }
            var color = Mathf.Sin(Time.realtimeSinceStartup * 15f) * 0.5f + 0.5f;
            _style.normal.textColor = new Color(1f, color, color, 1f);
            GUILayout.Label("PLAY MODE IS ACTIVE, CHANGES WON'T GET SAVED!", _style);
            Handles.EndGUI();
        }
    }
}