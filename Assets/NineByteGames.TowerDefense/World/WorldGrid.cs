﻿using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.Common.Extensions;
using NineByteGames.Common.Structures;
using NineByteGames.TowerDefense.World.Grid;
using UnityEngine;

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

    // TODO do this some other way
    public event Action<int, GridCoordinate> TileAdded;

    /// <summary>
    ///  Fill the given snapshot with the grid data starting with <paramref name="bottomLeft"/>
    /// </summary>
    /// <param name="snapshot"> The snapshot to fill. </param>
    /// <param name="bottomLeft"> The bottom left position of the snapshot. </param>
    public void Fill(Array2D<CellData> snapshot, GridCoordinate bottomLeft)
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