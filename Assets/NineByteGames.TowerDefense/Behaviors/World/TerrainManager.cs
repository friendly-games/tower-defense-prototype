using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Services;
using NineByteGames.TowerDefense.World.Grid;
using UnityEngine;
using UnityEngine.Serialization;

namespace NineByteGames.TowerDefense.Behaviors.World
{
  /// <summary> Manages all of the terrain tiles. </summary>
  public class TerrainManager : MonoBehaviour
  {
    /// <summary> All of the types of tiles present in the system. </summary>
    [FormerlySerializedAs("Tiles")]
    [Tooltip("All of the tiles that can be generated in the world")]
    public TileTemplate[] Templates;

    private GameObject _terrainParent;

    private GridChunk _gridChunk;

    /// <summary> Starts this object. </summary>
    public void Start()
    {
      Managers.Terrain = this;

      _terrainParent = gameObject;

      GenerateTerrain();
    }

    /// <summary> Generates the background for the game. </summary>
    private void GenerateTerrain()
    {
      const float factor = 1 / 10.0f;
      const int offset = 100;
      const int mapWidth = 50;

      _gridChunk = new GridChunk();

      for (int x = -mapWidth; x < mapWidth; x++)
      {
        for (int y = -mapWidth; y < mapWidth; y++)
        {
          var coordinate = new GridCoordinate(x, y);
          int type = (int) (Mathf.PerlinNoise(x * factor + offset, y * factor + offset) * Templates.Length);

          AddTile(type, coordinate);
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

      // then add it to unity
      var newTile = Templates[type].GameObject.Clone(coordinate.ToVector3(), Quaternion.identity);
      newTile.name = "Terrain @ " + coordinate;
      newTile.SetParent(_terrainParent);
    }
  }
}