using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.Common.Structures;

namespace NineByteGames.TowerDefense.World.Grid
{
  /// <summary> Represents the grid of the world. </summary>
  public class GridChunk
  {
    // todo OPTIMIZE by using a flat array
    private readonly Array2D<CellData> _data;

    private const int GridSize = 1024;
    private const int HalfGridSize = 1024 / 2;

    /// <summary> Default constructor. </summary>
    public GridChunk()
    {
      _data = new Array2D<CellData>(GridSize, GridSize);
    }

    /// <summary> T data at the specified grid coordinate. </summary>
    /// <param name="index"> Zero-based index of the entry to access. </param>
    public CellData this[GridCoordinate index]
    {
      get { return this[index.X, index.Z]; }
      set { this[index.X, index.Z] = value; }
    }

    /// <summary> T data at the specified grid coordinate. </summary>
    /// <param name="x"> The x coordinate of the position. </param>
    /// <param name="z"> The z coordinate of the position. </param>
    public CellData this[int x, int z]
    {
      get { return _data[x / (HalfGridSize), z / (HalfGridSize)]; }
      set { _data[x / (HalfGridSize), z / (HalfGridSize)] = value; }
    }
  }
}