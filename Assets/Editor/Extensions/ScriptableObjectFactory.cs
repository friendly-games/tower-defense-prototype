using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <summary>
///   Helper class for instantiating ScriptableObjects.
/// </summary>
public class ScriptableObjectFactory
{
  [MenuItem("Project/Create/ScriptableObject")]
  [MenuItem("Assets/Create/ScriptableObject")]
  public static void Create()
  {
    var assembly = GetAssembly();

    // Get all classes derived from ScriptableObject
    var types = from type in assembly.GetTypes()
                where type.IsSubclassOf(typeof(ScriptableObject))
                where !type.FullName.StartsWith("UnityTest")
                select type;

    // Show the selection window.
    var window = EditorWindow.GetWindow<ScriptableObjectWindow>(true, "Create a new ScriptableObject", true);
    window.ShowPopup();

    window.SetTypes(types.ToArray());
  }

  /// <summary>
  ///   Returns the assembly that contains the script code for this project (currently hard coded)
  /// </summary>
  private static Assembly GetAssembly()
  {
    return Assembly.Load(new AssemblyName("Assembly-CSharp"));
  }
}