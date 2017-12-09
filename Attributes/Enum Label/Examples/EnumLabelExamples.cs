
using UnityEngine;

public class EnumLabelExamples : ScriptableObject
{
    //------------------------------------------------------------------------------------//
    //----------------------------- ENUM DECLARATIONS ------------------------------------//
    //------------------------------------------------------------------------------------//

    public enum EnumExample
    {
        [EnumLabel("This Isn't The Default Enum Name O:")]
        ExampleEnumElement,
        [EnumLabel("Alternate Name #1 (Yes You Can Include Symbols Too)")]
        ExampleElementNameThatWillNeverBeSeenNumberTwo,
        [EnumLabel("Hello \"World\"")]
        EnumElementNumberThree,
        [EnumLabel("You Should See The Example Code")]
        LetsDoOneMoreBecauseWeCan
    }

    //------------------------------------------------------------------------------------//
    //----------------------------------- FIELDS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    [EnumLabel("This Is A Custom Enum (It Isn't Named Like This Internally)")]
    public EnumExample thisFieldNameWillBeDifferentAsWell;

}