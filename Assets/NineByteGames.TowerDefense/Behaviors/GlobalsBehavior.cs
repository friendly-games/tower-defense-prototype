using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors
{
  /// <summary> Responsible for setting up the global services and managers. </summary>
  internal class GlobalsBehavior : MonoBehaviour
  {
    [Tooltip("The parent to assign to new terrain tiles")]
    public GameObject TerrainParent;

    /// <summary> Initializes all of the managers. </summary>
    public void Start()
    {

    }
  }
}