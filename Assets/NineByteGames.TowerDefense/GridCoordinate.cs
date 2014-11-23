using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense
{
  /// <summary> Represents a position aligned to the grid. </summary>
  public struct GridCoordinate
  {
    /// <summary> The X coordinate of the position. </summary>
    public readonly int X;

    /// <summary> The Z coordinate of the position. </summary>
    public readonly int Z;

    /// <summary> Constructor. </summary>
    /// <param name="x"> The x coordinate of the position. </param>
    /// <param name="z"> The x coordinate of the position. </param>
    public GridCoordinate(int x, int z)
    {
      X = x;
      Z = z;
    }

    /// <summary> Creates a grid coordinate from a vector3. </summary>
    /// <param name="location"> The vector3 from which the coordinate should be created. </param>
    /// <returns> A GridCoordinate. </returns>
    public static GridCoordinate FromVector3(Vector3 location)
    {
      return new GridCoordinate(Mathf.FloorToInt(location.x), Mathf.FloorToInt(location.y));
    }

    /// <summary> Converts the coordinate into a vector3. </summary>
    /// <returns> This coordinate as a Vector3. </returns>
    public Vector3 ToVector3()
    {
      return new Vector3(X + 0.5f, Z + 0.5f, 0);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      unchecked
      {
        return (X * 397) ^ Z;
      }
    }

    /// <summary> Tests if this GridCoordinate is considered equal to another. </summary>
    public bool Equals(GridCoordinate other)
    {
      return X == other.X && Z == other.Z;
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      return obj is GridCoordinate
             && Equals((GridCoordinate) obj);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
      return String.Format("{0}, {1}", X, Z);
    }

    /// <summary> Add two world positions together. </summary>
    public static GridCoordinate operator +(GridCoordinate lhs, GridCoordinate rhs)
    {
      return new GridCoordinate(lhs.X + rhs.X, lhs.Z + rhs.Z);
    }

    /// <summary> Subtract two world positions </summary>
    public static GridCoordinate operator -(GridCoordinate lhs, GridCoordinate rhs)
    {
      return new GridCoordinate(lhs.X - rhs.X, lhs.Z - rhs.Z);
    }
  }
}