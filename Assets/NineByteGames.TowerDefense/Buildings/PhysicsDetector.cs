using System;
using System.Collections.Generic;
using NineByteGames.TowerDefense.World.Grid;
using UnityEngine;

namespace NineByteGames.TowerDefense.Buildings
{
  /// <summary> Detects physics object at specified coordinates. </summary>
  [Serializable]
  internal class PhysicsDetector
  {
    [Tooltip("The layers at which physics is checked")]
    public LayerMask m_Layers;

    /// <summary> Check if the given location is empty. </summary>
    /// <param name="lowerLeft"> The lower left where the object will be placed. </param>
    /// <param name="placeable"> The placeable that may be placed in the given location. </param>
    /// <returns> True if the given location is free from physics objects. </returns>
    public bool IsEmpty(GridCoordinate lowerLeft, PlaceableObject placeable)
    {
      var position = placeable.Strategy.ConvertToGameObjectPosition(lowerLeft);
      var size = placeable.Strategy.Size.ToVector2();

      // we don't want it to be one exactly because we want to avoid accidently colliding with
      // objects right next to ourselves.
      size.x -= 0.1f;
      size.y -= 0.1f;

      var result = Physics2D.BoxCast(position, size, 0, Vector2.right, 0, m_Layers.value);
      return result.collider == null;
    }
  }
}