using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Unity
{
  public static class LayerMaskExtensions
  {
    /// <summary> Check if a game object matches the given layer mask. </summary>
    /// <param name="layerMask"> The layerMask to act on. </param>
    /// <param name="gameObject"> The game object to check. </param>
    /// <returns>
    ///  true if the game object is on one of the layers in <paramref name="layerMask"/>, false if not.
    /// </returns>
    public static bool Contains(this LayerMask layerMask, GameObject gameObject)
    {
      return Layer.FromLayerId(gameObject.layer).IsIn(layerMask);
    }
  }
}