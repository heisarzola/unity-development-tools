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
