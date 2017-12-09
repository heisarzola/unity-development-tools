
/*---------------- Creation Date: 12-Jul-17 -----------------//
//------------ Last Modification Date: 12-Jul-17 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- String Builder Extension
 *       
 *   <<< DESCRIPTION >>>
 *       -- This file contains all of the Enum String Builder methods.
 *
 *   <<< LIMITATIONS >>>
 *       -- None.
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module: None.
//----------------------------------------------------------------------*/

/*------------------------- TABLE OF CONTENTS --------------------------*/
/*  EnumExtension
 *      <<< EMPTY >>>  
 *      -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
 *      <<< EMPTY >>>
 *      -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
 *      <<< CLASS EXTENSIONS >>>
 *         -- void Clear(this StringBuilder value)
 *         -- void Append(this StringBuilder value, params string[] toAppend)
 */
//----------------------------------------------------------------------*/

/*------------------------------- NOTES --------------------------------//
 *   <<< TO-DO LIST >>>
 *       -- <<< EMPTY >>>
 *
 *   <<< POSSIBLES >>>
 *       -- <<< EMPTY >>>
 *
 *   <<< SOURCES >>>
 *       -- [1] StringBuilder Clear : https://stackoverflow.com/questions/1709471/best-way-to-clear-contents-of-nets-stringbuilder
//---------------------------------------------------------------------*/

/*---------------------------- CHANGELOG -------------------------------//
 *   <<< V.1.0.0 -- 12-Jul-17 >>>
 *       -- Created class, added Append overrides and Clear methods.
//----------------------------------------------------------------------*/

using System.Text;

public static class StringBuilderExtension
{
    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    /// <summary>
    /// Clears the contents of the string builder.
    /// </summary>
    /// <param name="thisStringBuilder">The <see cref="StringBuilder"/> to clear. </param>
    public static void Clear(this StringBuilder thisStringBuilder) // [1]
    {
        thisStringBuilder.Length = 0;
        thisStringBuilder.Capacity = 0;
    }

    /// <summary>
    /// <para>Extension of the <see cref="StringBuilder"/> Append method to support more than one parameter.</para>
    /// </summary>
    public static void Append(this StringBuilder thisStringBuilder, params string[] toAppend)
    {
        for (int i = 0; i < toAppend.Length; i++)
        {
            thisStringBuilder.Append(toAppend[i]);
        }
    }

}//End of class