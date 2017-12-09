using UnityEngine;

// To enforce the referencing mono to have an animator to retrieve its parameters, use this attribute.
[RequireComponent(typeof(Animator))]
public class AnimatorParameterExample : MonoBehaviour
{
    [AnimatorParameter]
    public string anyTypeParameter;
    [AnimatorParameter(AnimatorParameterAttribute.ParameterType.Float)]
    public string floatParameter;
    [AnimatorParameter(AnimatorParameterAttribute.ParameterType.Int)]
    public string intParameter;
    [AnimatorParameter(AnimatorParameterAttribute.ParameterType.Bool)]
    public string boolParameter;
    [AnimatorParameter(AnimatorParameterAttribute.ParameterType.Trigger)]
    public string triggerParameter;
}