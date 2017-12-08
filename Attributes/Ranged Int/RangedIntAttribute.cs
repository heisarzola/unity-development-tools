
/*---------------- Creation Date: 13-Jul-16 -----------------//
//------------ Last Modification Date: 08-Dec-17 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Ranged Int Attribute.
 *       
 *   <<< DESCRIPTION >>>
 *       -- An editor use attribute that will allow showing ranged ints with a dual slider.
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
public class RangedIntAttribute : PropertyAttribute
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

    public int max;
    public int min;
    public RangeDisplayType rangeDisplayType;

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public RangedIntAttribute(int min, int max, RangeDisplayType rangeDisplayType = RangeDisplayType.LockedRanges)
    {
        this.min = min;
        this.max = max;
        this.rangeDisplayType = rangeDisplayType;
    }//End of RangedIntAttribute()

}//End of class