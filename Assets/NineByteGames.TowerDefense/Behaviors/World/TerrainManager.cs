using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Services;
using NineByteGames.TowerDefense.World;
using NineByteGames.TowerDefense.World.Grid;
using Pathfinding;
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
      Managers.Terrain = this;

      _terrainParent = gameObject;
      _grid = new WorldGrid();

      _grid.TileAdded += AddTile;

      _grid.Initialize(Templates.Length);

      StartCoroutine(TestMethod());
    }

    private IEnumerator TestMethod()
    {
      yield return new WaitForSeconds(1);

      GridUpdate.ResetGraph();
      GridUpdate.MarkWalkable(new GridCoordinate(0, 0), new Size(5, 3), false);
      Debug.Log("Done");
    }

    /// <summary> Creates a tile. </summary>
    /// <param name="type"> The type. </param>
    /// <param name="coordinate"> The coordinate. </param>
    private void AddTile(int type, GridCoordinate coordinate)
    {
      var newTile = Templates[type].GameObject.Clone(coordinate.ToVector3(), Quaternion.identity);
      newTile.name = "Terrain @ " + coordinate;
      newTile.SetParent(_terrainParent);
    }
  }
}