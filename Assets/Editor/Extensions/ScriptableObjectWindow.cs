using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

internal class EndNameEdit : EndNameEditAction
{
  #region implemented abstract members of EndNameEditAction

  public override void Action(int instanceId, string pathName, string resourceFile)
  {
    AssetDatabase.CreateAsset(EditorUtility.InstanceIDToObject(instanceId),
                              AssetDatabase.GenerateUniqueAssetPath(pathName));
  }

  #endregion
}

/// <summary>
///   Scriptable object window.
/// </summary>
public class ScriptableObjectWindow : EditorWindow
{
  private string[] _names;
  private int _selectedIndex;
  private Type[] _types;

  public void SetTypes(Type[] value)
  {
    _types = value;
    _names = _types.Select(type => String.Format("{0} ({1})", type.Name, type.FullName))
                   .ToArray();
  }

  public void OnGUI()
  {
    GUILayout.Label("ScriptableObject Class");
    _selectedIndex = EditorGUILayout.Popup(_selectedIndex, _names);

    if (GUILayout.Button("Create"))
    {
      var asset = CreateInstance(_types[_selectedIndex]);

      ProjectWindowUtil.StartNameEditingIfProjectWindowExists(asset.GetInstanceID(),
                                                              CreateInstance<EndNameEdit>(),
                                                              string.Format("{0}.asset", _names[_selectedIndex]),
                                                              AssetPreview.GetMiniThumbnail(asset),
                                                              null);

      Close();
    }
  }
}