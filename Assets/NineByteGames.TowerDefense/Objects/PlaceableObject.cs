using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NineByteGames.TowerDefense.Utils;
using NineByteGames.TowerDefense.World.Grid;
using UnityEngine;

namespace NineByteGames.TowerDefense.Objects
{
  internal class PlaceableObject : ScriptableObject
  {
    [Tooltip("The none-displayed name of the item")]
    public string Name;

    [Tooltip("The item that can be placed in the world")]
    public GameObject Template;

    [Tooltip("The item that displays when the user is attempting to show the item")]
    public GameObject PreviewItem;

    [Tooltip("The number of grid units the object takes up")]
    public ObjectShape Size;

    /// <summary>
    ///  Convert the given coordinate into a Vector3 that represents the location at which
    ///  <see cref="Template"/> would be placed if the lower left of <see cref="Template"/> existed at
    ///  <see cref="lowerLeft"/>.
    /// </summary>
    public Vector3 ConvertToGameObjectPosition(GridCoordinate lowerLeft)
    {
      switch (Size)
      {
        case ObjectShape.SquareUnit:
          return MathUtils.GetCenterOf1x1(lowerLeft);
        case ObjectShape.TwoByTwo:
          return MathUtils.GetCenterOf2x2(lowerLeft);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}