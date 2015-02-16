using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.Common.Structures;
using NineByteGames.TowerDefense.Behaviors.World;
using NineByteGames.TowerDefense.World.Grid;

namespace NineByteGames.TowerDefense.World
{
  /// <summary> Contains all of the chunks that exist in the world. </summary>
  internal class WorldGrid
  {
    // TODO use a proper grid with more than one chunk
    private readonly GridChunk _gridChunk;

    /// <summary> Default constructor. </summary>
    public WorldGrid()
    {
      _gridChunk = new GridChunk();
    }

    /// <summary>
    ///  Fill the given snapshot with the grid data starting with <paramref name="bottomLeft"/>
    /// </summary>
    /// <param name="bottomLeft"> The bottom left position of the snapshot. </param>
    /// <param name="snapshot"> The snapshot to fill. </param>
    public void Get(GridCoordinate bottomLeft, Array2D<CellData> snapshot)
    {
      // TODO check for error conditions

      int width = snapshot.Width;
      int height = snapshot.Height;
      int startX = bottomLeft.X;
      int startZ = bottomLeft.Z;

      var iterator = snapshot.GetIterator();

      for (int x = startX; x < startX + width; x++)
      {
        for (int z = startZ; z < startZ + height; z++)
        {
          iterator.Value = _gridChunk[new GridCoordinate(x, z)];
          iterator.MoveNext();
        }
      }
    }

    /// <summary> Sets an area to have the cell values given by <paramref name="cellData"/>. </summary>
    /// <param name="bottomLeft"> The bottom left position of the area. </param>
    /// <param name="size"> The size of the area to set the value of. </param>
    /// <param name="cellData"> The value that the area should be set to. </param>
    public void Set(GridCoordinate bottomLeft, Size size, CellData cellData)
    {
      // TODO check for error conditions

      int width = size.Width;
      int height = size.Height;
      int startX = bottomLeft.X;
      int startZ = bottomLeft.Z;

      for (int x = startX; x < startX + width; x++)
      {
        for (int z = startZ; z < startZ + height; z++)
        {
          _gridChunk[new GridCoordinate(x, z)] = cellData;
        }
      }

      GridUpdate.MarkCost(bottomLeft, size, cellData.RawType * 10);
    }

    /// <summary> Check if the given area of the grid is empty. </summary>
    /// <param name="lowerLeft"> The lower left starting position to check. </param>
    /// <param name="size"> The size of the area to check.  The entire area spans from
    ///  <paramref name="lowerLeft"/> to lowerLeft + size. </param>
    /// <returns> True if the area is empty and a building can be placed, false otherwise. </returns>
    public bool IsEmpty(GridCoordinate lowerLeft, Size size)
    {
      int startX = lowerLeft.X;
      int endX = startX + size.Width;
      int startZ = lowerLeft.Z;
      int endZ = startZ + size.Height;

      for (int x = lowerLeft.X; x < endX; x++)
      {
        for (int z = lowerLeft.Z; z < endZ; z++)
        {
          if (!_gridChunk[x, z].IsEmpty)
          {
            return false;
          }
        }
      }

      return true;
    }
  }
}