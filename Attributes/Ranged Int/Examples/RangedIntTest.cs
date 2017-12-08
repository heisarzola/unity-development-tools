using UnityEngine;

public class RangedIntTest : ScriptableObject
{
    [Header("Ranged Int Without Attribute")]
    public RangedInt exampleOne;

    [Header("Ranged Int With Locked Limits")]
    [RangedInt(0, 100, RangedIntAttribute.RangeDisplayType.LockedRanges)]
    public RangedInt exampleTwo;

    [Header("Ranged Int With Editable Limits")]
    [RangedInt(0, 100, RangedIntAttribute.RangeDisplayType.EditableRanges)]
    public RangedInt exampleThree;

    [Header("Ranged Int With Hidden (But Locked) Limits")]
    [RangedInt(0, 100, RangedIntAttribute.RangeDisplayType.HideRanges)]
    public RangedInt exampleFour;
}