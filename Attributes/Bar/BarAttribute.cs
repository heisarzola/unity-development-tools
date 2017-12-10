
/*---------------- Creation Date: 09-Dec-17 -----------------//
//------------ Last Modification Date: 09-Dec-17 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Bar Attribute.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Attribute in charge of containing the information that references a method call, color and current numerical value in order to draw a "progress" bar.
 *
 *   <<< LIMITATIONS >>>
 *       -- None.
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module: 
 *          -- None.
//----------------------------------------------------------------------*/

/*------------------------------- NOTES --------------------------------//
 *   <<< TO-DO LIST >>>
 *       -- <<< EMPTY >>>
 *
 *   <<< POSSIBLES >>>
 *       -- Include the possibility of drawing from a property, not only int and float fields.
 *
 *   <<< SOURCES >>>
 *       -- [1] Base Class : https://gist.github.com/LotteMakesStuff/2d3c6dc7a913ed118601db95735574de
//---------------------------------------------------------------------*/

/*---------------------------- CHANGELOG -------------------------------//
 *   <<< V.1.0.0 -- 09-Dec-17 >>>
 *       -- Created using the reference. [1]
 *       -- Improved by caching the expensive reflection operations. 
 *       -- Improved by ensuring the colored always remained visible.
 *       -- Improved by adding several new color options.
 *       -- Added ability to provide a custom initial value, and support properties and parameterless methods.
 *       -- Improved by allowing any value (floats were allowed only when less than 1).
//----------------------------------------------------------------------*/

using System.Collections.Generic;
using UnityEngine;

public class BarAttribute : PropertyAttribute
{
    //------------------------------------------------------------------------------------//
    //----------------------------- ENUM DECLARATIONS ------------------------------------//
    //------------------------------------------------------------------------------------//

    public enum BarColor
    {
        Red,
        Cyan,
        Blue,
        Lime,
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

    private static readonly Dictionary<BarColor, Color> _barColorToConcrete = new Dictionary<BarColor, Color>()
    {
        { BarColor.Red, Color.red},
        { BarColor.Orange, new Color32(255, 128, 0, 255)},
        { BarColor.Yellow, Color.yellow},
        { BarColor.Lime, Color.green},
        { BarColor.Green, new Color32(87, 116, 48, 255)},
        { BarColor.Cyan, Color.cyan},
        { BarColor.Blue, Color.blue},
        { BarColor.Pink, new Color32(255, 66, 160, 255)},
        { BarColor.Magenta, Color.magenta},
        { BarColor.Purple, new Color32(127, 0, 255, 255)},
        { BarColor.White, Color.white},
        { BarColor.Gray, Color.gray},
        { BarColor.Grey, Color.grey},
        { BarColor.Black, Color.black}
    };

    public readonly BarColor color;
    public readonly string maxValueName;
    public readonly float staticMaxValue;

    private readonly bool _usingStaticMaxValue = false;

    //------------------------------------------------------------------------------------//
    //--------------------------------- PROPERTIES ---------------------------------------//
    //------------------------------------------------------------------------------------//

    public bool UsingStaticMaxValue
    {
        get { return _usingStaticMaxValue; }
    }

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public static Color GetConcreteColor(BarColor key)
    {
        Color fetchedColor;

        if (_barColorToConcrete.TryGetValue(key, out fetchedColor))
        {
            return fetchedColor;
        }

        // No color
        return Color.clear;
    }

    public BarAttribute(int staticMaxValue, BarColor color = BarColor.Red)
    {
        _usingStaticMaxValue = true;
        this.staticMaxValue = staticMaxValue;
        this.color = color;
    }
    public BarAttribute(float staticMaxValue, BarColor color = BarColor.Red)
    {
        _usingStaticMaxValue = true;
        this.staticMaxValue = staticMaxValue;
        this.color = color;
    }
    public BarAttribute(double maxValue, BarColor color = BarColor.Red)
    {
        _usingStaticMaxValue = true;
        this.staticMaxValue = (float)maxValue;
        this.color = color;
    }

    public BarAttribute(string maxValueName = null, BarColor color = BarColor.Red)
    {
        this.maxValueName = maxValueName;
        this.color = color;
    }
}