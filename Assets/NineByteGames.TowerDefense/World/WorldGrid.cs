using System;
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

    /// <summary> Initialize the world creating the tiles as needed. </summary>
    /// <param name="numberOfTiles"> Number of tiles. </param>
    public void Initialize(int numberOfTiles)
    {
      const float factor = 1 / 10.0f;
      const int offset = 100;
      const int mapWidth = 50;
      // TODO make the tile count static (or at least not based on unity)

      for (int x = -mapWidth; x < mapWidth; x++)
      {
        for (int y = -mapWidth; y < mapWidth; y++)
        {
          var coordinate = new GridCoordinate(x, y);
          int type = (int) (Mathf.PerlinNoise(x * factor + offset, y * factor + offset) * numberOfTiles);

          AddTile(type, coordinate);
        }
      }
    }

    // TODO do this some other way
    public event Action<int, GridCoordinate> TileAdded;

    /// <summary>
    ///  Fill the given snapshot with the grid data starting with <paramref name="bottomLeft"/>
    /// </summary>
    /// <param name="snapshot"> The snapshot to fill. </param>
    /// <param name="bottomLeft"> The bottom left position of the snapshot. </param>
    public void Fill(Array2D<GridData> snapshot, GridCoordinate bottomLeft)
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

    /// <summary> Creates a tile. </summary>
    /// <param name="type"> The type. </param>
    /// <param name="coordinate"> The coordinate. </param>
    private void AddTile(int type, GridCoordinate coordinate)
    {
      // add it to the chunk
      _gridChunk[coordinate] = new GridData()
                               {
                                 Data = type
                               };

      TileAdded.InvokeSafe(type, coordinate);
    }
  }
}