
/*---------------- Creation Date: 11-Mar-17 ------------------//
//------------ Last Modification Date: 06-Dec-17 -------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- String Extension
 *       
 *   <<< DESCRIPTION >>>
 *       -- The string extension class contains several methods that .
 *
 *   <<< LIMITATIONS >>>
 *       -- None.
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module: StringBuilderExtension
//----------------------------------------------------------------------*/

/*------------------------------- NOTES --------------------------------//
*   <<< TO-DO LIST >>>
*       -- <<< EMPTY >>>
*
*   <<< POSSIBLES >>>
*       -- <<< EMPTY >>>
*
*   <<< SOURCES >>>
*       -- [1] String Split : http://stackoverflow.com/questions/4488969/split-a-string-by-capital-letters
*       -- [2] Case Insensitive Contains : https://stackoverflow.com/questions/444798/case-insensitive-containsstring
*       -- [3] Is Numeric : https://stackoverflow.com/questions/894263/how-do-i-identify-if-a-string-is-a-number
*       -- [4] Substitute Char : https://stackoverflow.com/questions/9367119/replacing-a-char-at-a-given-index-in-string
*       -- [5] Char Counting : https://stackoverflow.com/questions/5340564/counting-how-many-times-a-certain-char-appears-in-a-string-before-any-other-char
*       -- [6] Rich Text Formats : https://github.com/kir-avramenko/DebugLog-Helper
//---------------------------------------------------------------------*/

/*---------------------------- CHANGELOG -------------------------------//
 *   <<< V.1.0.0 -- 11-Mar-17 >>>
 *       -- Created class and added Replace methods for every Nth instance of a string / char.
 *   <<< V.1.0.1 -- 15-Apr-17 >>>
 *       -- Added From CamelCaseToSeparated.
 *   <<< V.1.0.2 -- 16-Jun-17 >>>
 *       -- Added Contains and IsNumeric.
 *   <<< V.1.0.3 -- 17-Jul-17 >>>
 *       -- Added a method to correctly append text.
 *   <<< V.1.0.4 -- 20-Aug-17 >>>
 *       -- Added the ability to count the times a given char appears on a string.
 *   <<< V.1.0.5 -- 06-Dec-17 >>>
 *       -- Updated some method names and implemented params usage on the Append method.
 //----------------------------------------------------------------------*/

using System.Text.RegularExpressions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

//------------------------------------------------------------------------------------//
//----------------------------- ENUM DECLARATIONS ------------------------------------//
//------------------------------------------------------------------------------------//

public enum RichTextColors
{
    aqua,
    black,
    blue,
    brown,
    cyan,
    darkblue,
    fuchsia,
    green,
    grey,
    lightblue,
    lime,
    magenta,
    maroon,
    navy,
    olive,
    purple,
    red,
    silver,
    teal,
    white,
    yellow
}

public static class StringExtension
{

    //------------------------------------------------------------------------------------//
    //----------------------------------- FIELDS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    private static readonly StringBuilder _stringBuilder = new StringBuilder();

    /*------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------*/

    #region Class Extensions

    /// <summary>
    /// <para>Checks whether or not a given string contains another string inside it.</para>
    /// </summary>
    /// <param name="thisString">Current string.</param>
    /// <param name="toLook">String that will be searched inside the selected string.</param>
    /// <param name="comparisonType">Criteria that will be used to search for the given string.</param>
    /// <returns></returns>
    public static bool Contains(this string thisString, string toLook, StringComparison comparisonType) // [2]
    {
        return thisString.IndexOf(toLook, comparisonType) >= 0;
    }

    /// <summary>
    /// <para>Determines if a given string is the representation of a number (and is possible to be casted as one).</para>
    /// </summary>
    /// <param name="thisString">Current string.</param>
    public static bool IsNumeric(this string thisString) // [3]
    {
        return thisString.All(Char.IsDigit);
    }

    /// <summary>
    /// Obtains the number of times a given char can be found within a string.
    /// </summary>
    /// <param name="thisString">Current string.</param>
    /// <param name="toLook">Char that will be counted.</param>
    /// <returns></returns>
    public static int GetCharFrequency(this string thisString, char toLook) // [5]
    {
        return Regex.Matches(thisString, toLook.ToString()).Count;
    }

    public static string ReplaceCharAtIndex(this string thisString, int indexToChange, char newValue) // [4]
    {
        _stringBuilder.Clear();
        _stringBuilder.Append(thisString);
        _stringBuilder[indexToChange] = newValue;
        return _stringBuilder.ToString();
    }

    /// <summary>
    /// <para>Replaces every Nth instance of a substring occurance within a string.</para>
    /// </summary>
    /// <param name="toLook">Substring to look for within the current string.</param>
    /// <param name="toReplaceWith">String that will replace the <see cref="toLook"/> string.</param>
    /// <param name="nthOccurance">
    /// <para>Occurance number that will get its to</para>
    /// <para>1 would mean that every occurance will be replaced, where 2 means every second (meaning, it will ignore the first, third, etc.).</para>
    /// </param>
    /// <param name="thisString">Current string.</param>
    public static string Replace(this string thisString, string toLook, string toReplaceWith, int nthOccurance)
    {
        string[] appearances = thisString.Split(new string[] { toLook }, StringSplitOptions.None);
        _stringBuilder.Clear();
        for (int i = 1; i <= appearances.Length; i++)
        {
            _stringBuilder.Append(appearances[i - 1]);
            _stringBuilder.Append((i % nthOccurance == 0) ? toReplaceWith : toLook.ToString());
        }
        return _stringBuilder.ToString();
    }

    /// <summary>
    /// <para>Replaces every Nth instance of a char occurance within a string.</para>
    /// </summary>
    /// <param name="toLook">Char to look for within the current string.</param>
    /// <param name="toReplaceWith">String that will replace the <see cref="toLook"/> string.</param>
    /// <param name="nthOccurance">
    /// <para>(OPTIONAL) Occurance number that will get its to</para>
    /// <para>1 would mean that every occurance will be replaced, where 2 means every second (meaning, it will ignore the first, third, etc.).</para>
    /// </param>
    /// <param name="thisString">Current string.</param>
    public static string Replace(this string thisString, char toLook, string toReplaceWith, int nthOccurance = 1)
    {
        string[] appearances = thisString.Split(toLook);
        _stringBuilder.Clear();
        for (int i = 1; i <= appearances.Length; i++)
        {
            _stringBuilder.Append(appearances[i - 1]);
            _stringBuilder.Append((i % nthOccurance == 0) ? toReplaceWith : toLook.ToString());
        }
        return _stringBuilder.ToString();
    }


    /// <summary>
    /// <para>Adds a given char after every Nth position.</para>
    /// <para>i.e. Adding '_' to the string "Hello World" every 2nd char will result in "He_ll_o _Wo_rl_d".</para>
    /// </summary>
    /// <param name="thisString">Current string.</param>
    /// <param name="toAdd">Character that will be added every Nth position.</param>
    /// <param name="nthOccurance">Frequency in which the character should be added.</param>
    /// <param name="invertedOccurance">
    /// <para>Inverts the positions in which the char will be added. Instead of counting from start to finish, it will be from finish to start.</para>
    /// <para>i.e. "Hello" with "_" after every 2nd char will be "H_el_lo" instead of "He_ll_o".</para>
    /// </param>
    /// <returns></returns>
    public static string AddCharEveryNthStep(this string thisString, char toAdd, int nthOccurance, bool invertedOccurance = false)
    {
        if (nthOccurance > 0)
        {
            UnityEngine.Debug.LogError("String Extension - AddCharEveryNthChar - Attempted to add a char in an invalid occurance rate (X < 0).");
        }

        StringBuilder result = new StringBuilder();

        int counter = 0;

        for (int i = (invertedOccurance ? thisString.Length - 1 : 0);
            (invertedOccurance ? i > -1 : i < thisString.Length);
            i += (invertedOccurance ? -1 : 1))
        {
            result.Append(thisString[i]);
            counter++;

            if (counter % nthOccurance == 0 && counter != (invertedOccurance ? thisString.Length : 0))
            {
                result.Append(toAdd);
            }
        }

        return (invertedOccurance ? result.ToString().Invert() : result.ToString());
    }

    /// <summary>
    /// <para>Invert the given string; Revert the order in which chars are found within a string.</para>
    /// <para>i.e. "Hello" will turn into "olleH".</para>
    /// </summary>
    /// <param name="thisString">Current string.</param>
    /// <returns></returns>
    public static string Invert(this string thisString)
    {
        _stringBuilder.Clear();

        for (int i = thisString.Length - 1; i > -1; i--)
        {
            _stringBuilder.Append(thisString[i]);
        }

        return _stringBuilder.ToString();
    }

    /// <summary>
    /// Concanetantes string in a garbage-free way, by making use of a static <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="thisString">Current string.</param>
    /// <param name="strings">Strings that will be concatenated, separated by a comma.</param>
    /// <returns></returns>
    public static string Append(this string thisString, params string[] strings)
    {
        _stringBuilder.Clear();
        _stringBuilder.Append(thisString);
        for (int i = 0; i < strings.Length; i++)
        {
            _stringBuilder.Append(strings[i]);
        }
        return _stringBuilder.ToString();
    }

    #endregion Class Extensions

    #region Rich Text Formats

    /// <summary>
    /// Surrounds the given text in the needed tags for rich text color, with the specified color.
    /// </summary>
    /// <param name="thisString">Current string.</param>
    /// <param name="color">Color that the text should be.</param>
    public static string AddRichTextTag_Color(this string thisString, RichTextColors color) // [6]
    {
        return string.Format("<color={0}>{1}</color>", color.ToString(), thisString);
    }

    /// <summary>
    /// Surrounds the given text in the needed tags for rich text size, with the specified size.
    /// </summary>
    /// <param name="thisString">Current string.</param>
    /// <param name="size">Size the text should be.</param>
    public static string AddRichTextTag_Size(this string thisString, int size) // [6]
    {
        return string.Format("<size={0}>{1}</size>", size, thisString);
    }

    /// <summary>
    /// Surrounds the given text in the needed tags for bold text.
    /// </summary>
    /// <param name="thisString">Current string.</param>
    public static string AddRichTextTag_Bold(this string thisString) // [6]
    {
        return string.Format("<b>{0}</b>", thisString);
    }

    /// <summary>
    /// Surrounds the given text in the needed tags for italics text.
    /// </summary>
    /// <param name="thisString">Current string.</param>
    public static string AddRichTextTag_Italics(this string thisString) // [6]
    {
        return string.Format("<i>{0}</i>", thisString);
    }

    #endregion Rich Text Formats

}//End of class