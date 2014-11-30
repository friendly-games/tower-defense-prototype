using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.World.Grid
{
  /// <summary> Represents the grid of the world. </summary>
  public class GridChunk
  {
    private readonly GridData[,] _data;
    private const int GridSize = 1024;
    private const int HalfGridSize = 1024 / 2;

    /// <summary> Default constructor. </summary>
    public GridChunk()
    {
      _data = new GridData[GridSize, GridSize];
    }

    /// <summary> T data at the specified grid coordinate. </summary>
    /// <param name="index"> Zero-based index of the entry to access. </param>
    public GridData this[GridCoordinate index]
    {
      get { return _data[index.X / (HalfGridSize), index.Z / (HalfGridSize)]; }
      set { _data[index.X / (HalfGridSize), index.Z / (HalfGridSize)] = value; }
    }

    /// <summary> T data at the specified grid coordinate. </summary>
    /// <param name="x"> The x coordinate of the position. </param>
    /// <param name="z"> The z coordinate of the position. </param>
    public GridData this[int x, int z]
    {
      get { return this[new GridCoordinate(x, z)]; }
      set { this[new GridCoordinate(x, z)] = value; }
    }
  }
}