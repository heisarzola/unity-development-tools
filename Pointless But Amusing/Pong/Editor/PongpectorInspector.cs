using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PongGame))]
[CanEditMultipleObjects]
public class PongpectorInspector : Editor
{
    SerializedProperty topRacketXProp;
    SerializedProperty bottomRacketXProp;

    public void OnEnable()
    {
        topRacketXProp = serializedObject.FindProperty("topRacketX");
        bottomRacketXProp = serializedObject.FindProperty("bottomRacketX");
        var pongpector = target as PongGame;
        EditorApplication.update += pongpector.UpdateFrame;
    }

    public void OnDisable()
    {
        var pongpector = target as PongGame;
        EditorApplication.update -= pongpector.UpdateFrame;
    }

    public override void OnInspectorGUI()
    {
        var pongpector = target as PongGame;
        serializedObject.Update();
        if (pongpector.isTopRacketValid)
        {
            topRacketXProp.floatValue = EditorGUILayout.Slider(topRacketXProp.floatValue, 0, 1);
        }
        GUILayout.Label(pongpector.texture);
        if (pongpector.isBottomRacketValid)
        {
            bottomRacketXProp.floatValue = EditorGUILayout.Slider(bottomRacketXProp.floatValue, 0, 1);
        }
        serializedObject.ApplyModifiedProperties();
    }

    public override bool RequiresConstantRepaint()
    {
        return true;
    }
}
