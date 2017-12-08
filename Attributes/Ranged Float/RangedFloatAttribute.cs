
/*---------------- Creation Date: 13-Jul-16 -----------------//
//------------ Last Modification Date: 08-Dec-17 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Ranged Float Attribute.
 *       
 *   <<< DESCRIPTION >>>
 *       -- An editor use attribute that will allow showing ranged floats with a dual slider.
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
 *       -- <<< EMPTY >>>
 *
 *   <<< SOURCES >>>
 *       -- [1] Idea of including editable limits: https://gist.github.com/LotteMakesStuff/0de9be35044bab97cbe79b9ced695585 
//---------------------------------------------------------------------*/

/*---------------------------- CHANGELOG -------------------------------//
 *   <<< V.1.0.0 -- 13-Jul-16 >>>
 *       -- Class creation.
 *   <<< V.1.0.1 -- 08-Dec-17 >>>
 *       -- Implemented options that allow editing/hiding limits if needed.
//----------------------------------------------------------------------*/

using UnityEngine;

[System.Serializable]
public class RangedFloatAttribute : PropertyAttribute
{

    //------------------------------------------------------------------------------------//
    //----------------------------- ENUM DECLARATIONS ------------------------------------//
    //------------------------------------------------------------------------------------//

    public enum RangeDisplayType // [1]
    {
        LockedRanges,
        EditableRanges,
        HideRanges
    }

    //------------------------------------------------------------------------------------//
    //----------------------------------- FIELDS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public float max;
    public float min;
    public RangeDisplayType rangeDisplayType;

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public RangedFloatAttribute(float min, float max, RangeDisplayType rangeDisplayType = RangeDisplayType.LockedRanges)
    {
        this.min = min;
        this.max = max;
        this.rangeDisplayType = rangeDisplayType;
    }//End of RangedFloatAttribute()

}//End of class