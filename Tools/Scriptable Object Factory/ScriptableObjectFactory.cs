
/*---------------- Creation Date: 15-Apr-17 -----------------//
//------------ Last Modification Date: 15-Apr-17 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Scriptable Object Factory.
 *       
 *   <<< DESCRIPTION >>>
 *       -- A helper class for instantiating ScriptableObjects in the editor.
 *
 *   <<< LIMITATIONS >>>
 *       -- None.
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module: 
 *          -- BaseSakuraVariableSO
 *          -- ScriptableObjectFactory
 *          -- EditableEntry
//----------------------------------------------------------------------*/

/*------------------------- TABLE OF CONTENTS --------------------------*/
/*  ScriptableObjectFactory
 *      <<< EMPTY >>> 
 *      -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
 *      <<< EMPTY >>>
 *      -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
 *      <<< PUBLIC STATIC METHODS >>>
 *          -- void CreateScriptableObject()
 *          -- Assembly GetAssembly()
 *          -- Assembly GetPluginAssembly()
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
 *       -- [1] Most of the class : https://github.com/liortal53/ScriptableObjectFactory/blob/master/Assets/Editor/ScriptableObjectFactory.cs
//---------------------------------------------------------------------*/

/*---------------------------- CHANGELOG -------------------------------//
 *   <<< V.1.0.0 -- 15-Apr-17 >>>
 *       -- Class creation.
//----------------------------------------------------------------------*/

using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <summary>
/// A helper class for instantiating ScriptableObjects in the editor.
/// </summary>
public class ScriptableObjectFactory// [1] 
{
    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    [MenuItem("Tools/Project/Scriptable Object Asset Creator")]
    public static void CreateScriptableObject()
    {
        // Get all classes derived from ScriptableObject
        var allScriptableObjects = (from t in ScriptableObjectFactory.GetAssembly().GetTypes()
                                    where t.IsSubclassOf(typeof(ScriptableObject))
                                    where !t.Name.Contains(typeof(EndNameEdit).ToString())
                                    where !t.IsSubclassOf(typeof(EditorWindow))
                                    where !t.IsAbstract
                                    where !t.IsGenericType
                                    select t).ToArray();

        Type[] allPluginScriptableObjects = new Type[0];

        Assembly pluginAssembly = ScriptableObjectFactory.GetPluginAssembly();

        if (pluginAssembly != null)
        {
            allPluginScriptableObjects = (from t in ScriptableObjectFactory.GetPluginAssembly().GetTypes()
                                          where t.IsSubclassOf(typeof(ScriptableObject))
                                          where !t.Name.Contains(typeof(EndNameEdit).ToString())
                                          where !t.IsSubclassOf(typeof(EditorWindow))
                                          where !t.IsAbstract
                                          where !t.IsGenericType
                                          select t).OrderBy(s => s.Name).ToArray();
        }

        // Show the selection window.
        ScriptableObjectFactoryWindow.Init(ConcatArrays(allScriptableObjects, allPluginScriptableObjects));
    }

    /// <summary>
    /// Returns the assembly that contains the script code for this project (currently hard coded)
    /// </summary>
    public static Assembly GetAssembly()
    {
        return Assembly.Load(new AssemblyName("Assembly-CSharp"));
    }

    public static Assembly GetPluginAssembly()
    {
        try
        {
            return Assembly.Load(new AssemblyName("Assembly-CSharp-firstpass"));
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// <para>Returns an array that is the combination of the two selected arrays.</para>
    /// </summary>
    /// <typeparam name="T">Array type that will be concatenated.</typeparam>
    /// <param name="array">Currently selected array.</param>
    /// <param name="otherArray">Other array that will be concatenated.</param>
    /// <returns></returns>
    /// <param name="ignoreDuplicates">(OPTIONAL) Should duplicated entries in the resulting array be ignored?</param>
    public static T[] ConcatArrays<T>(T[] array, T[] otherArray, bool ignoreDuplicates = false) // [1]
    {
        T[] resultantArray = new T[array.Length + otherArray.Length];

        Array.Copy(array, resultantArray, array.Length);
        Array.Copy(otherArray, 0, resultantArray, array.Length, otherArray.Length);

        return (ignoreDuplicates ? resultantArray.Distinct().ToArray() : resultantArray);
    }//End of ConcatArrays

}