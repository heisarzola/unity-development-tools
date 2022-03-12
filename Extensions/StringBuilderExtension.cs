

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- String Builder Extension
 *       
 *   <<< DESCRIPTION >>>
 *       -- This file contains all of the Enum String Builder methods.
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