
/*---------------- Creation Date: 13-Jul-16 -----------------//
//------------ Last Modification Date: 08-Dec-17 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Ranged Float.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Class intended to be an alternative to Vector2 for inspector usage of ranged values.
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
 *   <<< V.1.0.0 -- 13-Jul-16 >>>
 *       -- Class creation.
 *   <<< V.1.0.1 -- 08-Dec-17 >>>
 *       -- Implemented options that allow editing/hiding limits if needed.
//----------------------------------------------------------------------*/

using UnityEngine;

[System.Serializable]
public class RangedFloat
{
    //------------------------------------------------------------------------------------//
    //----------------------------------- FIELDS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public float min;
    public float max;

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//


    public void Init()
    {
        min = 0;
        max = 1;
    }//End of Init()

    public RangedFloat()
    {
        Init();
    }//End of MinMaxFloat()

    public RangedFloat(float min, float max)
    {
        this.min = min;
        this.max = max;
    }//End of MinMaxFloat(float min, float max)

    /// <summary>
    /// Gets a random value within the min and max range.
    /// </summary>
    /// <returns></returns>
    public float GetRandomValue()
    {
        return Random.Range(min, max);
    }//End of getRandomValue


    public override string ToString()
    {
        return string.Format("[Class: {0}, Min: {1}, Max: {2}]", typeof(RangedFloat).Name, min, max);
    }//End of ToString()

    /// <summary>
    /// Implicit operator that will automatically fetch a random value within range when a <see cref="RangedFloat"/> is used as a <see cref="float"/>.
    /// </summary>
    /// <param name="someRangedFloat"></param>
    public static implicit operator float(RangedFloat someRangedFloat)
    {
        return someRangedFloat.GetRandomValue();
    }//End of implicit operator float

}//End of class