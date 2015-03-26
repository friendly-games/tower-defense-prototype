using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors
{
  /// <summary> Responsible for setting up the global services and managers. </summary>
  internal class GlobalsBehavior : MonoBehaviour
  {
    [Tooltip("The parent to assign to new terrain tiles")]
    public GameObject TerrainParent;

    [Tooltip("The Global UI object that should be activated on start")]
    public GameObject GlobalUI;

    /// <summary> Initializes all of the managers. </summary>
    public void Start()
    {
      GlobalUI.SetActive(true);
    }
  }
}