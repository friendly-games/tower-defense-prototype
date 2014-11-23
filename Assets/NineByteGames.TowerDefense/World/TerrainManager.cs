using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.World
{
  /// <summary> Manages all of the terrain tiles. </summary>
  internal class TerrainManager : MonoBehaviour
  {
    public GameObject[] Tiles;

    private GameObject _terrainParent;

    /// <summary> Starts this object. </summary>
    public void Start()
    {
      Instance = this;

      _terrainParent = gameObject;

      float factor = 1 / 10.0f;

      const int offset = 100;

      const int mapWidth = 50;

      for (int x = -mapWidth; x < mapWidth; x++)
        for (int y = -mapWidth; y < mapWidth; y++)
        {
          int type = (int) (Mathf.PerlinNoise(x * factor + offset, y * factor + offset) * Tiles.Length);

          var newTile = Tiles[type].Clone(new GridCoordinate(x, y).ToVector3(), Quaternion.identity);
          newTile.SetParent(_terrainParent);
        }
    }

    /// <summary> Singleton for the game. </summary>
    public static TerrainManager Instance { get; private set; }
  }
}