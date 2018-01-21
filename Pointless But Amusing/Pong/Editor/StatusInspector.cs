using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PongGameStatus))]
public class StatusInspector : Editor
{
    public override void OnInspectorGUI()
    {
        var status = target as PongGameStatus;
        EditorGUILayout.IntField("Score", status.score);
        EditorGUILayout.IntField("Ball", status.ball);
        if (GUILayout.Button("Restart"))
        {
            status.Restart();
        }
    }
}
