using System;
using System.Collections;

namespace NineByteGames.TowerDefense
{
  /// <summary> Represents the grid of the world. </summary>
  public class Grid
  {
    private readonly GridData[,] _data;
    private const int GridSize = 1024;
    private const int HalfGridSize = 1024 / 2;

    public Grid()
    {
      _data = new GridData[GridSize, GridSize];
    }

    /// <summary> Get the data at the specified index. </summary>
    /// <param name="index"> Zero-based index of the entry to access. </param>
    public GridData this[GridCoordinate index]
    {
      get { return _data[index.X / (HalfGridSize), index.Z / (HalfGridSize)]; }
      set { _data[index.X / (HalfGridSize), index.Z / (HalfGridSize)] = value; }
    }
  }
}