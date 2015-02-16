using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.Common.Structures;
using NineByteGames.TowerDefense.World;
using NineByteGames.TowerDefense.World.Grid;
using UnityEngine;

namespace NineByteGames.TowerDefense.Objects
{
  /// <summary> The shape of a placeable object in the world. </summary>
  internal enum ObjectShape
  {
    SquareUnit,
    TwoByTwo,
  }

  internal interface IObjectShapeStrategy
  {
    /// <summary> The shape for which this strategy is valid. </summary>
    ObjectShape Shape { get; }

    /// <summary> The size of the object when placed in the world. </summary>
    Size Size { get; }

    /// <summary> Converts a lowerLeft to a game object position. </summary>
    /// <param name="lowerLeft"> The lower left. </param>
    /// <returns> The given data converted to a game object position. </returns>
    Vector3 ConvertToGameObjectPosition(GridCoordinate lowerLeft);

    /// <summary> Converts an object position to a lower left position. </summary>
    /// <param name="vector3"> The position to convert. </param>
    /// <returns> The lower left of the object. </returns>
    GridCoordinate ConvertFromGameObjectPosition(Vector3 vector3);
  }
}