using UnityEngine;

public class SceneNameExample : ScriptableObject
{
    [SceneName(true)]
    public string oneActiveScene;

    [SceneName(false)]
    public string anyScene;
}