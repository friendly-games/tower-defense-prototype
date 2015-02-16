using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Objects;
using NineByteGames.TowerDefense.Services;
using NineByteGames.TowerDefense.Unity;
using NineByteGames.TowerDefense.World;
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

    private WorldGrid _grid;

    /// <summary> Starts this object. </summary>
    public void Start()
    {
      _terrainParent = gameObject;
      _grid = new WorldGrid();
      GridUpdate.ResetGraph();

      Managers.Terrain = this;
      Managers.Placer = new ObjectWorldPlacement(_grid);
    }

    /// <summary> Creates a tile. </summary>
    /// <param name="type"> The type. </param>
    /// <param name="coordinate"> The coordinate. </param>
    private void AddTile(int type, GridCoordinate coordinate)
    {
      //var newTile = Templates[type].GameObject.Clone(coordinate.ToVector3(), Quaternion.identity);
      //newTile.name = "Terrain @ " + coordinate;
      //newTile.SetParent(_terrainParent);
    }
  }
}