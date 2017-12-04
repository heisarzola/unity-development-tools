
/*---------------- Creation Date: 19-Jan-17 -----------------//
//------------ Last Modification Date: 19-Jan-17 ------------//
//------ Luis Raul Arzola Lopez : http://heisarzola.com ------*/

using System.Text;
using UnityEngine;

public class InspectorMethodsExamples : MonoBehaviour
{

    #region Method One - Bool

    [InspectorMethod("MethodOne", buttonName = "Method 1 - Bool Parameter", useValue = true)]
    public bool testBool;

    public void MethodOne(bool myBool)
    {
        Debug.Log(myBool ? "Test Bool is True" : "Test Bool is False");
    }

    #endregion Method One - Bool

    #region Method Two - String

    [InspectorMethod("MethodTwo", buttonName = "Method 2 - String Parameter", useValue = true)]
    public string testString;

    public void MethodTwo(string testString)
    {
        StringBuilder sb = new StringBuilder("Test string is: ");
        Debug.Log(sb.Append(testString).ToString());
    }

    #endregion Method Two - String

    #region Method Three - Parameterless

    [InspectorMethod("MethodThree", buttonName = "Method 3 - Parameterless", useValue = false)]
    public string thisCouldBeAnyVariableType;

    public void MethodThree()
    {
        Debug.Log("Called the parameterless method.");
    }

    #endregion Method Three - Parameterless

    #region Method Four - Unexistent

    [InspectorMethod("UnexistentMethodName", buttonName = "This Button Name Won't Even Appear", useValue = false)]
    public string someVariableThatWontBeUsed;

    #endregion Method Four - Unexistent

}//End of class