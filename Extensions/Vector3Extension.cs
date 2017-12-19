
/*---------------- Creation Date: 04-Jun-17 -----------------//
//------------ Last Modification Date: 18-Dec-17 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Vector3 Extension
 *       
 *   <<< DESCRIPTION >>>
 *       -- This file contains all of the Vector3 extension methods.
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
/*  Vector3Extension
 *      <<< EMPTY >>>  
 *      -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
 *      <<< EMPTY >>>
 *      -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
 *      <<< CLASS EXTENSIONS >>>
 *          -- Vector3 GetOffsettedValue(this Vector3 thisVector3, float x, float y, float z)
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
 *       -- Class creation and GetOffsettedValue implementation.
 *   <<< V.1.0.1 -- 18-Dec-17 >>>
 *       -- Added Sub Vector methods.
//----------------------------------------------------------------------*/

using UnityEngine;

public static class Vector3Extension
{
    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    /// <summary>
    /// <para>Get a <see cref="Vector3"/> with an offsetted position by the given amount.</para>
    /// </summary>
    /// <param name="x">Amount of units in the X axis that will be offsetted from the current value.</param>
    /// <param name="y">Amount of units in the Y axis that will be offsetted from the current value.</param>
    /// <param name="z">Amount of units in the Z axis that will be offsetted from the current value.</param>
    /// <returns></returns>
    public static Vector3 GetOffsettedValue(this Vector3 thisVector3, float x, float y, float z)
    {
        return new Vector3(thisVector3.x + x, thisVector3.y + y, thisVector3.z + z);
    }//End of FitsInside(this Vector2 value, Vector2 other)


    public static Vector3 SetX(this Vector3 thisVector3, float x)
    {
        thisVector3.x = x;
        return thisVector3;
    }

    public static Vector3 SetY(this Vector3 thisVector3, float y)
    {
        thisVector3.y = y;
        return thisVector3;
    }

    public static Vector3 SetZ(this Vector3 thisVector3, float z)
    {
        thisVector3.z = z;
        return thisVector3;
    }

    #region Sub Vectors

    #region Sub Vectors > To Vector 2

    #region Sub Vectors > To Vector 2 > Add Missing Element

    public static Vector2 X_(this Vector3 v3, float missingElement = 0f) { return new Vector2(v3.x, missingElement); }
    public static Vector2 _X(this Vector3 v3, float missingElement = 0f) { return new Vector2(missingElement, v3.x); }
    public static Vector2 Y_(this Vector3 v3, float missingElement = 0f) { return new Vector2(v3.y, missingElement); }
    public static Vector2 _Y(this Vector3 v3, float missingElement = 0f) { return new Vector2(missingElement, v3.y); }
    public static Vector2 Z_(this Vector3 v3, float missingElement = 0f) { return new Vector2(v3.z, missingElement); }
    public static Vector2 _Z(this Vector3 v3, float missingElement = 0f) { return new Vector2(missingElement, v3.z); }

    #endregion Sub Vectors > To Vector 2 > Add Missing Element

    #region Sub Vectors > To Vector 2 > Mixed

    public static Vector2 XX(this Vector3 v3) { return new Vector2(v3.x, v3.x); }
    public static Vector2 XY(this Vector3 v3) { return new Vector2(v3.x, v3.y); }
    public static Vector2 XZ(this Vector3 v3) { return new Vector2(v3.x, v3.z); }
    public static Vector2 YX(this Vector3 v3) { return new Vector2(v3.y, v3.x); }
    public static Vector2 YY(this Vector3 v3) { return new Vector2(v3.y, v3.y); }
    public static Vector2 YZ(this Vector3 v3) { return new Vector2(v3.y, v3.z); }
    public static Vector2 ZX(this Vector3 v3) { return new Vector2(v3.z, v3.x); }
    public static Vector2 ZY(this Vector3 v3) { return new Vector2(v3.z, v3.y); }
    public static Vector2 ZZ(this Vector3 v3) { return new Vector2(v3.z, v3.z); }

    #endregion Sub Vectors >  To Vector 2 > Mixed

    #endregion Sub Vectors >  To Vector 2

    #region Sub Vectors > To Vector 3

    #region Sub Vectors > To Vector 3 > Mixed

    public static Vector3 XXX(this Vector3 v3) { return new Vector3(v3.x, v3.x, v3.x); }
    public static Vector3 YXX(this Vector3 v3) { return new Vector3(v3.y, v3.x, v3.x); }
    public static Vector3 ZXX(this Vector3 v3) { return new Vector3(v3.z, v3.x, v3.x); }
    public static Vector3 XYX(this Vector3 v3) { return new Vector3(v3.x, v3.y, v3.x); }
    public static Vector3 YYX(this Vector3 v3) { return new Vector3(v3.y, v3.y, v3.x); }
    public static Vector3 ZYX(this Vector3 v3) { return new Vector3(v3.z, v3.y, v3.x); }
    public static Vector3 XZX(this Vector3 v3) { return new Vector3(v3.x, v3.z, v3.x); }
    public static Vector3 YZX(this Vector3 v3) { return new Vector3(v3.y, v3.z, v3.x); }
    public static Vector3 ZZX(this Vector3 v3) { return new Vector3(v3.z, v3.z, v3.x); }
    public static Vector3 XXY(this Vector3 v3) { return new Vector3(v3.x, v3.x, v3.y); }
    public static Vector3 YXY(this Vector3 v3) { return new Vector3(v3.y, v3.x, v3.y); }
    public static Vector3 ZXY(this Vector3 v3) { return new Vector3(v3.z, v3.x, v3.y); }
    public static Vector3 XYY(this Vector3 v3) { return new Vector3(v3.x, v3.y, v3.y); }
    public static Vector3 YYY(this Vector3 v3) { return new Vector3(v3.y, v3.y, v3.y); }
    public static Vector3 ZYY(this Vector3 v3) { return new Vector3(v3.z, v3.y, v3.y); }
    public static Vector3 XZY(this Vector3 v3) { return new Vector3(v3.x, v3.z, v3.y); }
    public static Vector3 YZY(this Vector3 v3) { return new Vector3(v3.y, v3.z, v3.y); }
    public static Vector3 ZZY(this Vector3 v3) { return new Vector3(v3.z, v3.z, v3.y); }
    public static Vector3 XXZ(this Vector3 v3) { return new Vector3(v3.x, v3.x, v3.z); }
    public static Vector3 YXZ(this Vector3 v3) { return new Vector3(v3.y, v3.x, v3.z); }
    public static Vector3 ZXZ(this Vector3 v3) { return new Vector3(v3.z, v3.x, v3.z); }
    public static Vector3 XYZ(this Vector3 v3) { return new Vector3(v3.x, v3.y, v3.z); }
    public static Vector3 YYZ(this Vector3 v3) { return new Vector3(v3.y, v3.y, v3.z); }
    public static Vector3 ZYZ(this Vector3 v3) { return new Vector3(v3.z, v3.y, v3.z); }
    public static Vector3 XZZ(this Vector3 v3) { return new Vector3(v3.x, v3.z, v3.z); }
    public static Vector3 YZZ(this Vector3 v3) { return new Vector3(v3.y, v3.z, v3.z); }
    public static Vector3 ZZZ(this Vector3 v3) { return new Vector3(v3.z, v3.z, v3.z); }

    #endregion Sub Vectors > To Vector 3 > Mixed

    #endregion Sub Vectors > To Vector 3

    #region Sub Vectors > To Vector 4

    #region Sub Vectors > To Vector 4 > Mixed

    public static Vector4 XXXX(this Vector3 v3) { return new Vector4(v3.x, v3.x, v3.x, v3.x); }
    public static Vector4 YXXX(this Vector3 v3) { return new Vector4(v3.y, v3.x, v3.x, v3.x); }
    public static Vector4 ZXXX(this Vector3 v3) { return new Vector4(v3.z, v3.x, v3.x, v3.x); }
    public static Vector4 XYXX(this Vector3 v3) { return new Vector4(v3.x, v3.y, v3.x, v3.x); }
    public static Vector4 YYXX(this Vector3 v3) { return new Vector4(v3.y, v3.y, v3.x, v3.x); }
    public static Vector4 ZYXX(this Vector3 v3) { return new Vector4(v3.z, v3.y, v3.x, v3.x); }
    public static Vector4 XZXX(this Vector3 v3) { return new Vector4(v3.x, v3.z, v3.x, v3.x); }
    public static Vector4 YZXX(this Vector3 v3) { return new Vector4(v3.y, v3.z, v3.x, v3.x); }
    public static Vector4 ZZXX(this Vector3 v3) { return new Vector4(v3.z, v3.z, v3.x, v3.x); }
    public static Vector4 XXYX(this Vector3 v3) { return new Vector4(v3.x, v3.x, v3.y, v3.x); }
    public static Vector4 YXYX(this Vector3 v3) { return new Vector4(v3.y, v3.x, v3.y, v3.x); }
    public static Vector4 ZXYX(this Vector3 v3) { return new Vector4(v3.z, v3.x, v3.y, v3.x); }
    public static Vector4 XYYX(this Vector3 v3) { return new Vector4(v3.x, v3.y, v3.y, v3.x); }
    public static Vector4 YYYX(this Vector3 v3) { return new Vector4(v3.y, v3.y, v3.y, v3.x); }
    public static Vector4 ZYYX(this Vector3 v3) { return new Vector4(v3.z, v3.y, v3.y, v3.x); }
    public static Vector4 XZYX(this Vector3 v3) { return new Vector4(v3.x, v3.z, v3.y, v3.x); }
    public static Vector4 YZYX(this Vector3 v3) { return new Vector4(v3.y, v3.z, v3.y, v3.x); }
    public static Vector4 ZZYX(this Vector3 v3) { return new Vector4(v3.z, v3.z, v3.y, v3.x); }
    public static Vector4 XXZX(this Vector3 v3) { return new Vector4(v3.x, v3.x, v3.z, v3.x); }
    public static Vector4 YXZX(this Vector3 v3) { return new Vector4(v3.y, v3.x, v3.z, v3.x); }
    public static Vector4 ZXZX(this Vector3 v3) { return new Vector4(v3.z, v3.x, v3.z, v3.x); }
    public static Vector4 XYZX(this Vector3 v3) { return new Vector4(v3.x, v3.y, v3.z, v3.x); }
    public static Vector4 YYZX(this Vector3 v3) { return new Vector4(v3.y, v3.y, v3.z, v3.x); }
    public static Vector4 ZYZX(this Vector3 v3) { return new Vector4(v3.z, v3.y, v3.z, v3.x); }
    public static Vector4 XZZX(this Vector3 v3) { return new Vector4(v3.x, v3.z, v3.z, v3.x); }
    public static Vector4 YZZX(this Vector3 v3) { return new Vector4(v3.y, v3.z, v3.z, v3.x); }
    public static Vector4 ZZZX(this Vector3 v3) { return new Vector4(v3.z, v3.z, v3.z, v3.x); }
    public static Vector4 XXXY(this Vector3 v3) { return new Vector4(v3.x, v3.x, v3.x, v3.y); }
    public static Vector4 YXXY(this Vector3 v3) { return new Vector4(v3.y, v3.x, v3.x, v3.y); }
    public static Vector4 ZXXY(this Vector3 v3) { return new Vector4(v3.z, v3.x, v3.x, v3.y); }
    public static Vector4 XYXY(this Vector3 v3) { return new Vector4(v3.x, v3.y, v3.x, v3.y); }
    public static Vector4 YYXY(this Vector3 v3) { return new Vector4(v3.y, v3.y, v3.x, v3.y); }
    public static Vector4 ZYXY(this Vector3 v3) { return new Vector4(v3.z, v3.y, v3.x, v3.y); }
    public static Vector4 XZXY(this Vector3 v3) { return new Vector4(v3.x, v3.z, v3.x, v3.y); }
    public static Vector4 YZXY(this Vector3 v3) { return new Vector4(v3.y, v3.z, v3.x, v3.y); }
    public static Vector4 ZZXY(this Vector3 v3) { return new Vector4(v3.z, v3.z, v3.x, v3.y); }
    public static Vector4 XXYY(this Vector3 v3) { return new Vector4(v3.x, v3.x, v3.y, v3.y); }
    public static Vector4 YXYY(this Vector3 v3) { return new Vector4(v3.y, v3.x, v3.y, v3.y); }
    public static Vector4 ZXYY(this Vector3 v3) { return new Vector4(v3.z, v3.x, v3.y, v3.y); }
    public static Vector4 XYYY(this Vector3 v3) { return new Vector4(v3.x, v3.y, v3.y, v3.y); }
    public static Vector4 YYYY(this Vector3 v3) { return new Vector4(v3.y, v3.y, v3.y, v3.y); }
    public static Vector4 ZYYY(this Vector3 v3) { return new Vector4(v3.z, v3.y, v3.y, v3.y); }
    public static Vector4 XZYY(this Vector3 v3) { return new Vector4(v3.x, v3.z, v3.y, v3.y); }
    public static Vector4 YZYY(this Vector3 v3) { return new Vector4(v3.y, v3.z, v3.y, v3.y); }
    public static Vector4 ZZYY(this Vector3 v3) { return new Vector4(v3.z, v3.z, v3.y, v3.y); }
    public static Vector4 XXZY(this Vector3 v3) { return new Vector4(v3.x, v3.x, v3.z, v3.y); }
    public static Vector4 YXZY(this Vector3 v3) { return new Vector4(v3.y, v3.x, v3.z, v3.y); }
    public static Vector4 ZXZY(this Vector3 v3) { return new Vector4(v3.z, v3.x, v3.z, v3.y); }
    public static Vector4 XYZY(this Vector3 v3) { return new Vector4(v3.x, v3.y, v3.z, v3.y); }
    public static Vector4 YYZY(this Vector3 v3) { return new Vector4(v3.y, v3.y, v3.z, v3.y); }
    public static Vector4 ZYZY(this Vector3 v3) { return new Vector4(v3.z, v3.y, v3.z, v3.y); }
    public static Vector4 XZZY(this Vector3 v3) { return new Vector4(v3.x, v3.z, v3.z, v3.y); }
    public static Vector4 YZZY(this Vector3 v3) { return new Vector4(v3.y, v3.z, v3.z, v3.y); }
    public static Vector4 ZZZY(this Vector3 v3) { return new Vector4(v3.z, v3.z, v3.z, v3.y); }
    public static Vector4 XXXZ(this Vector3 v3) { return new Vector4(v3.x, v3.x, v3.x, v3.z); }
    public static Vector4 YXXZ(this Vector3 v3) { return new Vector4(v3.y, v3.x, v3.x, v3.z); }
    public static Vector4 ZXXZ(this Vector3 v3) { return new Vector4(v3.z, v3.x, v3.x, v3.z); }
    public static Vector4 XYXZ(this Vector3 v3) { return new Vector4(v3.x, v3.y, v3.x, v3.z); }
    public static Vector4 YYXZ(this Vector3 v3) { return new Vector4(v3.y, v3.y, v3.x, v3.z); }
    public static Vector4 ZYXZ(this Vector3 v3) { return new Vector4(v3.z, v3.y, v3.x, v3.z); }
    public static Vector4 XZXZ(this Vector3 v3) { return new Vector4(v3.x, v3.z, v3.x, v3.z); }
    public static Vector4 YZXZ(this Vector3 v3) { return new Vector4(v3.y, v3.z, v3.x, v3.z); }
    public static Vector4 ZZXZ(this Vector3 v3) { return new Vector4(v3.z, v3.z, v3.x, v3.z); }
    public static Vector4 XXYZ(this Vector3 v3) { return new Vector4(v3.x, v3.x, v3.y, v3.z); }
    public static Vector4 YXYZ(this Vector3 v3) { return new Vector4(v3.y, v3.x, v3.y, v3.z); }
    public static Vector4 ZXYZ(this Vector3 v3) { return new Vector4(v3.z, v3.x, v3.y, v3.z); }
    public static Vector4 XYYZ(this Vector3 v3) { return new Vector4(v3.x, v3.y, v3.y, v3.z); }
    public static Vector4 YYYZ(this Vector3 v3) { return new Vector4(v3.y, v3.y, v3.y, v3.z); }
    public static Vector4 ZYYZ(this Vector3 v3) { return new Vector4(v3.z, v3.y, v3.y, v3.z); }
    public static Vector4 XZYZ(this Vector3 v3) { return new Vector4(v3.x, v3.z, v3.y, v3.z); }
    public static Vector4 YZYZ(this Vector3 v3) { return new Vector4(v3.y, v3.z, v3.y, v3.z); }
    public static Vector4 ZZYZ(this Vector3 v3) { return new Vector4(v3.z, v3.z, v3.y, v3.z); }
    public static Vector4 XXZZ(this Vector3 v3) { return new Vector4(v3.x, v3.x, v3.z, v3.z); }
    public static Vector4 YXZZ(this Vector3 v3) { return new Vector4(v3.y, v3.x, v3.z, v3.z); }
    public static Vector4 ZXZZ(this Vector3 v3) { return new Vector4(v3.z, v3.x, v3.z, v3.z); }
    public static Vector4 XYZZ(this Vector3 v3) { return new Vector4(v3.x, v3.y, v3.z, v3.z); }
    public static Vector4 YYZZ(this Vector3 v3) { return new Vector4(v3.y, v3.y, v3.z, v3.z); }
    public static Vector4 ZYZZ(this Vector3 v3) { return new Vector4(v3.z, v3.y, v3.z, v3.z); }
    public static Vector4 XZZZ(this Vector3 v3) { return new Vector4(v3.x, v3.z, v3.z, v3.z); }
    public static Vector4 YZZZ(this Vector3 v3) { return new Vector4(v3.y, v3.z, v3.z, v3.z); }
    public static Vector4 ZZZZ(this Vector3 v3) { return new Vector4(v3.z, v3.z, v3.z, v3.z); }

    #endregion Sub Vectors > To Vector 4 > Mixed

    #endregion Sub Vectors > To Vector 4

    #endregion Sub Vectors

}//End of class