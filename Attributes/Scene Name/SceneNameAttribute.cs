

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Scene Name Attribute.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Attribute that holds the filters used when searching through the included scenes in the project.
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module: 
 *          -- None.
//----------------------------------------------------------------------*/

using UnityEngine;

public class SceneNameAttribute : PropertyAttribute
{
    //------------------------------------------------------------------------------------//
    //----------------------------------- FIELDS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public int selectedValue = 0;
    public bool activeScenesOnly = true;

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public SceneNameAttribute(bool activeScenesOnly = true)
    {
        this.activeScenesOnly = activeScenesOnly;
    }

}//End of class