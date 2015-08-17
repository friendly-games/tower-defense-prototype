using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Buildings;
using NineByteGames.TowerDefense.World;
using NineByteGames.TowerDefense.World.Grid;
using UnityEngine;
using UnityEngine.Serialization;

namespace NineByteGames.TowerDefense.Behaviors.World
{
  /// <summary> Manages all of the terrain tiles. </summary>
  internal class TerrainManager : MonoBehaviour
  {
    /// <summary> All of the types of tiles present in the system. </summary>
    [FormerlySerializedAs("Templates")]
    [Tooltip("All of the tiles that can be generated in the world")]
    public TileTemplate[] m_Templates;

    [Tooltip("Determines if objects are in the way of placable objects")]
    public PhysicsDetector m_PlaceableObjectChecker;

    private WorldGrid _grid;

    /// <summary> Starts this object. </summary>
    public void Start()
    {
      _grid = new WorldGrid();
      GridUpdate.ResetGraph();
    }
  }
}