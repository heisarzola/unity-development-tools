using UnityEngine;

public class RangedFloatTest : ScriptableObject
{
    [Header("Ranged Float Without Attribute")]
    public RangedFloat exampleOne;

    [Header("Ranged Float With Locked Limits")]
    [RangedFloat(0, 1, RangedFloatAttribute.RangeDisplayType.LockedRanges)]
    public RangedFloat exampleTwo;

    [Header("Ranged Float With Editable Limits")]
    [RangedFloat(0, 1, RangedFloatAttribute.RangeDisplayType.EditableRanges)]
    public RangedFloat exampleThree;

    [Header("Ranged Float With Hidden (But Locked) Limits")]
    [RangedFloat(0, 1, RangedFloatAttribute.RangeDisplayType.HideRanges)]
    public RangedFloat exampleFour;
}