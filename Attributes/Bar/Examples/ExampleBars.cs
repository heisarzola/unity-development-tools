// example compoent

using UnityEngine;
using BarColor = BarAttribute.BarColor;

public class ExampleBars : ScriptableObject
{

    // Example 1
    [Header("Constant Max (100)")]
    [Bar(100, BarColor.Red)]
    public float example1Current = 50;

    // Example 2
    [Header("Max Defined By Other Field")]
    [Bar("example2Max", BarColor.Orange)]
    public float example2Current = 50f;
    public float example2Max = 100f;

    // Example 3
    [Header("Max Defined By Method (See Code)")]
    [Bar("MaxDefinedByMethod", BarColor.Yellow)]
    public float example3Current = 50f;
    public float example3MaxPart1 = 100f;
    public float example3MaxPart2 = 100f;

    public float MaxDefinedByMethod()
    {
        return example3MaxPart1 + example3MaxPart2;
    }

    // Example 4
    [Header("Max Defined By Property (See Code)")]
    [Bar("MaxDefinedByProperty", BarColor.Green)]
    public float example4Current;
    public float example4MaxPart1 = 100f;
    public float example4MaxPart2 = 100f;

    public float MaxDefinedByProperty
    {
        get
        {
            return example4MaxPart1 + example4MaxPart2;
        }
    }

}