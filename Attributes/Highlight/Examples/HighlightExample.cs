using UnityEngine;
using HighlightColor = HighlightAttribute.HighlightColor;

public class HighlightExample : ScriptableObject
{
    // This field will ALWAYS be highlighted to the default color, red.
    [Header("Always Highlighted")]
    [Highlight]
    public string alwaysOnDefaultColor;

    // This field will ALWAYS be highlighted to the SELECTED color, orange.
    [Highlight(HighlightColor.Orange)]
    public string alwaysOn;

    // Example of calling a parameterless method, (it can change its value in runtime).
    [Header("Highlighted When Condition Fulfilled")]
    [Highlight(HighlightColor.Yellow, "HighlightedWhenTrue")]
    public bool whenTrue;

    private bool HighlightedWhenTrue()
    {
        return whenTrue;
    }

    // Example of a parameterless method that only highlights when in runtime.
    [Highlight(HighlightColor.White, "TrueWhenTen")]
    public int whenTen;

    private bool TrueWhenTen()
    {
        return whenTen == 10;
    }

    [Highlight(HighlightColor.Cyan, "WhenAllAreTrue")]
    public bool whenThese3True;

    private bool WhenAllAreTrue()
    {
        return TrueWhenTen() && whenTrue && whenThese3True;
    }

    // Example of a parameterless method that only highlights when in runtime.
    [Header("Only On Runtime")]
    [Highlight(HighlightColor.Blue, "TrueAtRuntime")]
    public string onRuntime;

    private bool TrueAtRuntime()
    {
        return Application.isPlaying;
    }

    [Header("Multiple Parameters (See Code)")]
    [Highlight(HighlightColor.Purple, "MultipleParameters", true, true, "ordinary", true)]
    public string multipleParameters;

    bool MultipleParameters(bool just, bool an, string ordinary, bool example)
    {
        return just && an && example;
    }
}