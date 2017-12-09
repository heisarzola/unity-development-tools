
/*---------------- Creation Date: 18-Jul-16 -----------------//
//------------ Last Modification Date: 18-Jul-16 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Animator Parameter Attribute.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Attribute that specifies the currently selected item and item type from a list of animator parameters via the functionality provided by its drawer.
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
 *       -- [1] Drawer Idea : https://github.com/anchan828/property-drawer-collection/tree/master/AnimatorParameter
//---------------------------------------------------------------------*/

/*---------------------------- CHANGELOG -------------------------------//
 *   <<< V.1.0.0 -- 18-Jul-16 >>>
 *       -- Esentially copy/pasted the source code, the original developer did a good job.
//----------------------------------------------------------------------*/

using UnityEngine;

public class AnimatorParameterAttribute : PropertyAttribute // [1]
{
    //------------------------------------------------------------------------------------//
    //----------------------------- ENUM DECLARATIONS ------------------------------------//
    //------------------------------------------------------------------------------------//

    /// <summary>
    /// Type of the mecanim parameter, the numerical value is used for bitwise operations.
    /// </summary>
    public enum ParameterType
    {
        Float = 1,
        Int = 3,
        Bool = 4,
        Trigger = 9,
        None = 9999,
    }

    //------------------------------------------------------------------------------------//
    //----------------------------------- FIELDS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public ParameterType parameterType = ParameterType.None;
    public int selectedValue = 0;

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public AnimatorParameterAttribute(ParameterType parameterType = ParameterType.None)
    {
        this.parameterType = parameterType;
    }

}//End of class