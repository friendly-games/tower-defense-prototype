using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors.World
{
  /// <summary> Contains the template for a tile in the world. </summary>
  [Serializable]
  public struct TileTemplate
  {
    [Tooltip("The name of the given template.")]
    public string Name;

    [Tooltip("The game object template associated with the tile.")]
    public GameObject GameObject;

    [Tooltip("True if the title type does not allow objects through its path")]
    public bool DoesBlockPath;
  }
}