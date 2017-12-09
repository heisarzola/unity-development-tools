using UnityEngine;

public class ReadOnlyExample : ScriptableObject
{
    [Header("Public Fields")]
    public string _public;

    [ReadOnly]
    public string publicReadOnly = "You Cannot Edit This In The Inspector";

    [Header("Private Serialized Fields")]
    [SerializeField]
    private string _private;

    [ReadOnly]
    [SerializeField]
    private string _privateReadOnly = "Cannot Edit This Either";
}