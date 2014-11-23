using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

[ExecuteInEditMode]
internal class ValidateEditFunctionality : MonoBehaviour
{
  public void Update()
  {
    if (Application.isPlaying)
    {
      Object.Destroy(this);
      return;
    }

    Debug.Log("Validating in Editor");

    var gameObjectsWithEditBehavior = from gameObject in FindObjectsOfType<GameObject>()
                                      where gameObject.activeInHierarchy
                                      let editorBehavior = gameObject.RetrieveOwnObject<IEditorBehavior>()
                                      where editorBehavior != null
                                      select editorBehavior;

    foreach (var editorBehavior in gameObjectsWithEditBehavior)
    {
      editorBehavior.ValidateInEditor();
    }
  }
}

public interface IEditorBehavior
{
  void ValidateInEditor();
}