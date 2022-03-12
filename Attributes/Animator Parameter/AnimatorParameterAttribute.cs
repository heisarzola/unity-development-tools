

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Animator Parameter Attribute.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Attribute that specifies the currently selected item and item type from a list of animator parameters via the functionality provided by its drawer.
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module: 
 *          -- None.
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