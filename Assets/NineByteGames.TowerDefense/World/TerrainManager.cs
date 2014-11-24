using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NineByteGames.TowerDefense.World
{
  /// <summary> Manages all of the terrain tiles. </summary>
  internal class TerrainManager : MonoBehaviour
  {
    /// <summary> All of the types of tiles present in the system. </summary>
    public GameObject[] Tiles;

    private GameObject _terrainParent;

    /// <summary> Singleton for the game. </summary>
    public static TerrainManager Instance { get; private set; }

    /// <summary> Starts this object. </summary>
    public void Start()
    {
      Instance = this;

      _terrainParent = gameObject;

      GenerateTerrain();
    }

    /// <summary> Generates the background for the game. </summary>
    private void GenerateTerrain()
    {
      const float factor = 1 / 10.0f;
      const int offset = 100;
      const int mapWidth = 50;

      for (int x = -mapWidth; x < mapWidth; x++)
      {
        for (int y = -mapWidth; y < mapWidth; y++)
        {
          int type = (int) (Mathf.PerlinNoise(x * factor + offset, y * factor + offset) * Tiles.Length);

          var coordinate = new GridCoordinate(x, y);
          var newTile = Tiles[type].Clone(coordinate.ToVector3(), Quaternion.identity);
          newTile.name = "Terrain @ " + coordinate;
          newTile.SetParent(_terrainParent);
        }
      }
    }
  }
}