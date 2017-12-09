
/*---------------- Creation Date: 18-Jul-16 -----------------//
//------------ Last Modification Date: 18-Jul-16 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Scene Name Attribute.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Attribute that holds the filters used when searching through the included scenes in the project.
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
 *       -- <<< EMPTY >>>
//---------------------------------------------------------------------*/

/*---------------------------- CHANGELOG -------------------------------//
 *   <<< V.1.0.0 -- 18-Jul-16 >>>
 *       -- Empty class creation.
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