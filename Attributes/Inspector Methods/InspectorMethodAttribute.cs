
/*---------------- Creation Date: 19-Jan-17 -----------------//
//------------ Last Modification Date: 19-Jan-17 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

using UnityEngine;
using System.Reflection;

public class InspectorMethodAttribute : PropertyAttribute
{
    //------------------------------------------------------------------------------------//
    //----------------------------------- FIELDS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public string methodName;
    public string buttonName;
    public bool useValue;
    public BindingFlags flags;

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//


    public InspectorMethodAttribute(string methodName, string buttonName, bool useValue, BindingFlags flags = BindingFlags.Public | BindingFlags.Instance)
    {
        this.methodName = methodName;
        this.buttonName = buttonName;
        this.useValue = useValue;
        this.flags = flags;
    }

    public InspectorMethodAttribute(string methodName, bool useParameterBelow, BindingFlags flags)
        : this(methodName, methodName, useParameterBelow, flags)
    {
    }

    public InspectorMethodAttribute(string methodName, bool useParameterBelow)
        : this(methodName, methodName, useParameterBelow)
    {
    }

    public InspectorMethodAttribute(string methodName, string buttonName, BindingFlags flags)
        : this(methodName, buttonName, false, flags)
    {
    }

    public InspectorMethodAttribute(string methodName, string buttonName)
        : this(methodName, buttonName, false)
    {
    }

    public InspectorMethodAttribute(string methodName, BindingFlags flags)
        : this(methodName, methodName, false, flags)
    {
    }

    public InspectorMethodAttribute(string methodName)
        : this(methodName, methodName, false)
    {
    }

}//End of class