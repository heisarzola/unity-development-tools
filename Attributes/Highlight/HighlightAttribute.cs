

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Highlight Attribute.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Attribute in charge of containing the information that references a method call, color and parameters in order to highlight an area in a color when needed.
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module: 
 *          -- None.
//----------------------------------------------------------------------*/


using System.Collections.Generic;
using UnityEngine;

public class HighlightAttribute : PropertyAttribute
{
    //------------------------------------------------------------------------------------//
    //----------------------------- ENUM DECLARATIONS ------------------------------------//
    //------------------------------------------------------------------------------------//

    public enum HighlightColor
    {
        Red,
        Cyan,
        Blue,
        Green,
        Yellow,
        Pink,
        Magenta,
        Purple,
        Orange,
        White,
        Gray,
        Grey,
        Black
    }

    //------------------------------------------------------------------------------------//
    //----------------------------------- FIELDS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    private static readonly Dictionary<HighlightColor, Color> _highlightColorToConcrete = new Dictionary<HighlightColor, Color>()
    {
        { HighlightColor.Red, Color.red},
        { HighlightColor.Orange, new Color32(255, 128, 0, 255)},
        { HighlightColor.Yellow, Color.yellow},
        { HighlightColor.Green, Color.green},
        { HighlightColor.Cyan, Color.cyan},
        { HighlightColor.Blue, Color.blue},
        { HighlightColor.Pink, new Color32(255, 66, 160, 255)},
        { HighlightColor.Magenta, Color.magenta},
        { HighlightColor.Purple, new Color32(127, 0, 255, 255)},
        { HighlightColor.White, Color.white},
        { HighlightColor.Gray, Color.gray},
        { HighlightColor.Grey, Color.grey},
        { HighlightColor.Black, Color.black}
    };

    public HighlightColor highlightColor;
    public string validateMethod;
    public object[] methodParameters;

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public static Color GetConcreteColor(HighlightColor key)
    {
        Color fetchedColor;

        if (_highlightColorToConcrete.TryGetValue(key, out fetchedColor))
        {
            return fetchedColor;
        }

        // No color
        return Color.clear;
    }

    public static Color TextColorToUse(HighlightColor backgroundColor)
    {
        switch (backgroundColor)
        {
            case HighlightColor.Purple:
            case HighlightColor.Black:
            case HighlightColor.Grey:
            case HighlightColor.Gray:
            case HighlightColor.Blue:
                return Color.white;
            default:
                return Color.black;
        }
    }

    public HighlightAttribute(HighlightColor highlightColor = HighlightColor.Red, string validateMethod = null, params object[] methodParameters)
    {
        this.highlightColor = highlightColor;
        this.validateMethod = validateMethod;
        this.methodParameters = methodParameters;
    }
}