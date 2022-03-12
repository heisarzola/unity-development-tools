

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Ranged Int.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Class intended to be an alternative to Vector2 for inspector usage of ranged values.
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module: 
 *          -- None.
//----------------------------------------------------------------------*/

using UnityEngine;

[System.Serializable]
public class RangedInt
{
    //------------------------------------------------------------------------------------//
    //----------------------------------- FIELDS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public int min;
    public int max;

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public void Init()
    {
        min = 0;
        max = 1;
    }//End of Init()


    public RangedInt()
    {
        Init();
    }//End of MinMaxFloat()


    public RangedInt(int min, int max)
    {
        this.min = min;
        this.max = max;
    }//End of MinMaxFloat(float min, float max)


    public int GetRandomValue()
    {
        return Random.Range(min, max);
    }//End of getRandomValue


    public override string ToString()
    {
        return string.Format("[Class: {0}, Min: {1}, Max: {2}]", typeof(RangedInt).Name, min, max);
    }//End of ToString()


    public static implicit operator int(RangedInt someRangedInt)
    {
        return someRangedInt.GetRandomValue();
    }//End of implicit operator float

}//End of MinMaxFloat