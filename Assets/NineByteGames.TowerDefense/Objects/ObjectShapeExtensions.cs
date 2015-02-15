using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.World;

namespace NineByteGames.TowerDefense.Objects
{
  internal static class ObjectShapeExtensions
  {
    /// <summary> Gets the size of the given placeable size </summary>
    /// <param name="objectShape"> The shape of the given object. </param>
    /// <exception cref="ArgumentOutOfRangeException"> Thrown when one or more arguments are outside
    ///  the required range. </exception>
    /// <returns> The size of given shape. </returns>
    public static Size CalculateSize(this ObjectShape objectShape)
    {
      switch (objectShape)
      {
        case ObjectShape.SquareUnit:
          return new Size(1, 1);
        case ObjectShape.TwoByTwo:
          return new Size(2, 2);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}