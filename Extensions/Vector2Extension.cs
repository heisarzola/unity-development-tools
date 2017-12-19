
/*---------------- Creation Date: 04-Jun-17 -----------------//
//------------ Last Modification Date: 18-Dec-17 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Vector2 Extension
 *       
 *   <<< DESCRIPTION >>>
 *       -- This file contains all of the Vector2 extension methods.
 *
 *   <<< LIMITATIONS >>>
 *       -- None.
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module: 
 *          -- Vector2
 *          -- Vector3
 *          -- Vector4
//----------------------------------------------------------------------*/

/*------------------------- TABLE OF CONTENTS --------------------------*/
/*  Vector2Extension
 *      <<< EMPTY >>>  
 *      -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
 *      <<< EMPTY >>>
 *      -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
 *      <<< CLASS EXTENSIONS >>>
 *          -- bool FitsInside(this Vector2 thisVector2, Vector2 other)
 *          -- bool IsPointWithinArea(this Vector2 thisVector2, Vector2 other, Vector2 v2reaToCheck)
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
*       -- <<< EMPTY >>>
//---------------------------------------------------------------------*/

/*---------------------------- CHANGELOG -------------------------------//
 *   <<< V.1.0.0 -- 04-Jun-17 >>>
 *       -- Class creation and FitInside and IsPointWithinArea implementations.
 *   <<< V.1.0.1 -- 18-Dec-17 >>>
 *       -- Added Sub Vector methods.
//----------------------------------------------------------------------*/

using UnityEngine;

public static class Vector2Extension
{
    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    /// <summary>
    /// <para>Returns true when current <see cref="Vector2"/> fits inside "<see cref="other"/>".</para>
    /// <para>Both <see cref="Vector2"/>s are taken as size dimensions.</para>
    /// </summary>
    /// <param name="other">Target <see cref="Vector2"/> that will be checked to see if it is big enough to fit current <see cref="Vector2"/> or not.</param>
    public static bool FitsInside(this Vector2 thisVector2, Vector2 other)
    {
        if (thisVector2.x > other.x || thisVector2.y > other.y)
        {
            return false;
        }

        return true;
    }//End of FitsInside(this Vector2 value, Vector2 other)

    /// <summary>
    /// <para>Check if a <see cref="Vector2"/> point is within a certain area of another <see cref="Vector2"/> point.</para>
    /// <para>Essentially, creates a rectangle with the current <see cref="Vector2"/> as its center (with the given dimensions parameter) and sees if another <see cref="Vector2"/> point is within said rectangle.</para>
    /// </summary>
    /// <param name="other">Point that will be checked to be inside or outside the imaginary created rectangle.</param>
    /// <param name="areaToCheck">Dimensions of the imaginary rectangle that will be created, if "<see cref="other"/>" is located within this imaginary rectangle the method will return true.</param>
    /// <returns></returns>
    public static bool IsPointWithinArea(this Vector2 thisVector2, Vector2 other, Vector2 areaToCheck)
    {
        if (thisVector2.x - areaToCheck.x > other.x
            || thisVector2.x + areaToCheck.x < other.x)
        {
            return false;
        }

        if (thisVector2.y - areaToCheck.y > other.y
           || thisVector2.y + areaToCheck.y < other.y)
        {
            return false;
        }

        return true;
    }//End of IsPointWithinArea(this Vector2 value, Vector2 other, Vector2 v2reaToCheck)

    public static Vector2 SetX(this Vector2 thisVector2, float x)
    {
        thisVector2.x = x;
        return thisVector2;
    }

    public static Vector2 SetY(this Vector2 thisVector2, float y)
    {
        thisVector2.y = y;
        return thisVector2;
    }

    #region Sub Vectors

    #region Sub Vectors > To Vector 2

    #region Sub Vectors > To Vector 2 > Add Missing Element

    public static Vector2 X_(this Vector2 v2, float missingElement = 0f) { return new Vector2(v2.x, missingElement); }
    public static Vector2 _X(this Vector2 v2, float missingElement = 0f) { return new Vector2(missingElement, v2.x); }
    public static Vector2 Y_(this Vector2 v2, float missingElement = 0f) { return new Vector2(v2.y, missingElement); }
    public static Vector2 _Y(this Vector2 v2, float missingElement = 0f) { return new Vector2(missingElement, v2.y); }

    #endregion Sub Vectors > To Vector 2 > Add Missing Element 

    #region Sub Vectors > To Vector 2 > Mixed

    public static Vector2 XX(this Vector2 v2) { return new Vector2(v2.x, v2.x); }
    public static Vector2 YX(this Vector2 v2) { return new Vector2(v2.y, v2.x); }
    public static Vector2 XY(this Vector2 v2) { return new Vector2(v2.x, v2.y); }
    public static Vector2 YY(this Vector2 v2) { return new Vector2(v2.y, v2.y); }

    #endregion Sub Vectors > To Vector 2 > Mixed

    #endregion Sub Vectors > To Vector 2

    #region Sub Vectors > To Vector 3

    #region Sub Vectors > To Vector 3 > Add Missing Element

    public static Vector3 XY_(this Vector2 v2, float missingElement = 0) { return new Vector3(v2.x, v2.y, missingElement); }
    public static Vector3 X_Y(this Vector2 v2, float missingElement = 0) { return new Vector3(v2.x, missingElement, v2.y); }
    public static Vector3 _XY(this Vector2 v2, float missingElement = 0) { return new Vector3(missingElement, v2.x, v2.y); }
    public static Vector3 YX_(this Vector2 v2, float missingElement = 0) { return new Vector3(v2.y, v2.x, missingElement); }
    public static Vector3 Y_X(this Vector2 v2, float missingElement = 0) { return new Vector3(v2.y, missingElement, v2.x); }
    public static Vector3 _YX(this Vector2 v2, float missingElement = 0) { return new Vector3(missingElement, v2.y, v2.x); }

    #endregion Sub Vectors > To Vector 3 > Add Missing Element

    #region Sub Vectors > To Vector 3 > Mixed

    public static Vector3 XXX(this Vector2 v2) { return new Vector3(v2.x, v2.x, v2.x); }
    public static Vector3 YXX(this Vector2 v2) { return new Vector3(v2.y, v2.x, v2.x); }
    public static Vector3 XYX(this Vector2 v2) { return new Vector3(v2.x, v2.y, v2.x); }
    public static Vector3 YYX(this Vector2 v2) { return new Vector3(v2.y, v2.y, v2.x); }
    public static Vector3 XXY(this Vector2 v2) { return new Vector3(v2.x, v2.x, v2.y); }
    public static Vector3 YXY(this Vector2 v2) { return new Vector3(v2.y, v2.x, v2.y); }
    public static Vector3 XYY(this Vector2 v2) { return new Vector3(v2.x, v2.y, v2.y); }
    public static Vector3 YYY(this Vector2 v2) { return new Vector3(v2.y, v2.y, v2.y); }

    #endregion Sub Vectors > To Vector 3 > Mixed

    #endregion Sub Vectors > To Vector 3

    #region Sub Vectors > To Vector 4

    #region Sub Vectors > To Vector 4 > Mixed

    public static Vector4 XXXX(this Vector2 v2) { return new Vector4(v2.x, v2.x, v2.x, v2.x); }
    public static Vector4 YXXX(this Vector2 v2) { return new Vector4(v2.y, v2.x, v2.x, v2.x); }
    public static Vector4 XYXX(this Vector2 v2) { return new Vector4(v2.x, v2.y, v2.x, v2.x); }
    public static Vector4 YYXX(this Vector2 v2) { return new Vector4(v2.y, v2.y, v2.x, v2.x); }
    public static Vector4 XXYX(this Vector2 v2) { return new Vector4(v2.x, v2.x, v2.y, v2.x); }
    public static Vector4 YXYX(this Vector2 v2) { return new Vector4(v2.y, v2.x, v2.y, v2.x); }
    public static Vector4 XYYX(this Vector2 v2) { return new Vector4(v2.x, v2.y, v2.y, v2.x); }
    public static Vector4 YYYX(this Vector2 v2) { return new Vector4(v2.y, v2.y, v2.y, v2.x); }
    public static Vector4 XXXY(this Vector2 v2) { return new Vector4(v2.x, v2.x, v2.x, v2.y); }
    public static Vector4 YXXY(this Vector2 v2) { return new Vector4(v2.y, v2.x, v2.x, v2.y); }
    public static Vector4 XYXY(this Vector2 v2) { return new Vector4(v2.x, v2.y, v2.x, v2.y); }
    public static Vector4 YYXY(this Vector2 v2) { return new Vector4(v2.y, v2.y, v2.x, v2.y); }
    public static Vector4 XXYY(this Vector2 v2) { return new Vector4(v2.x, v2.x, v2.y, v2.y); }
    public static Vector4 YXYY(this Vector2 v2) { return new Vector4(v2.y, v2.x, v2.y, v2.y); }
    public static Vector4 XYYY(this Vector2 v2) { return new Vector4(v2.x, v2.y, v2.y, v2.y); }
    public static Vector4 YYYY(this Vector2 v2) { return new Vector4(v2.y, v2.y, v2.y, v2.y); }

    #endregion Sub Vectors > To Vector 4 > Mixed

    #endregion Sub Vectors > To Vector 4

    #endregion Sub Vectors


}//End of class