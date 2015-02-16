using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.Common.Structures;
using NineByteGames.TowerDefense.Utils;
using NineByteGames.TowerDefense.World;
using NineByteGames.TowerDefense.World.Grid;
using UnityEngine;

namespace NineByteGames.TowerDefense.Objects
{
  /// <summary> Allows retrieving of an IObjectShapeStrategy for a given ObjectShape.  </summary>
  internal static class ObjectShapeStrategies
  {
    private static readonly IObjectShapeStrategy SquareUnitStrategy;
    private static readonly IObjectShapeStrategy TwoByTwoStrategy;

    static ObjectShapeStrategies()
    {
      SquareUnitStrategy = new Strategy(new Size(1, 1),
                                        ObjectShape.SquareUnit,
                                        MathUtils.GetCenterOf1x1,
                                        MathUtils.GetGridCoordinateFor1x1);
      TwoByTwoStrategy = new Strategy(new Size(2, 2),
                                      ObjectShape.TwoByTwo,
                                      MathUtils.GetCenterOf2x2,
                                      MathUtils.GetGridCoordinateFor2x2);
    }

    /// <summary> Retrieves a IObjectShapeStrategy for a given ObjectShape. </summary>
    /// <exception cref="ArgumentOutOfRangeException"> Thrown when one or more arguments are outside
    ///  the required range. </exception>
    /// <param name="shape"> The shape for which a IObjectShapeStrategy should be retrieved. </param>
    /// <returns> The strategy for the given ObjectShape. </returns>
    public static IObjectShapeStrategy GetStrategyFor(ObjectShape shape)
    {
      switch (shape)
      {
        case ObjectShape.SquareUnit:
          return SquareUnitStrategy;
        case ObjectShape.TwoByTwo:
          return TwoByTwoStrategy;
        default:
          throw new ArgumentOutOfRangeException("shape");
      }
    }

    private class Strategy : IObjectShapeStrategy
    {
      private readonly Converter<GridCoordinate, Vector3> _convertToGameObjectPosition;
      private readonly Converter<Vector3, GridCoordinate> _convertToGridCoordinate;

      public Strategy(Size size,
                      ObjectShape shape,
                      Converter<GridCoordinate, Vector3> convertToGameObjectPosition,
                      Converter<Vector3, GridCoordinate> convertToGridCoordinate)
      {
        Size = size;
        Shape = shape;
        _convertToGameObjectPosition = convertToGameObjectPosition;
        _convertToGridCoordinate = convertToGridCoordinate;
      }

      public ObjectShape Shape { get; private set; }

      public Size Size { get; private set; }

      public Vector3 ConvertToGameObjectPosition(GridCoordinate lowerLeft)
      {
        return _convertToGameObjectPosition(lowerLeft);
      }

      public GridCoordinate ConvertFromGameObjectPosition(Vector3 vector3)
      {
        return _convertToGridCoordinate(vector3);
      }
    }
  }
}