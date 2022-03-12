

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Ranged Int Attribute.
 *       
 *   <<< DESCRIPTION >>>
 *       -- An editor use attribute that will allow showing ranged ints with a dual slider.
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module: 
 *          -- None.
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