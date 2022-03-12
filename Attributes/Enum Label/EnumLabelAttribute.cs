

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Enum Label Attribute.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Attribute that holds a temporal name to replace the contents of an enum entry to be anything you like. Functionality provided by drawer.
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module: 
 *          -- None.
//----------------------------------------------------------------------*/

using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
public class EnumLabelAttribute : PropertyAttribute // [1]
{
    //------------------------------------------------------------------------------------//
    //----------------------------------- FIELDS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public string label;

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public EnumLabelAttribute(string label)
    {
        this.label = label;
    }
}//End of class