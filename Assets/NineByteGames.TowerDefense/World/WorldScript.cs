using System.Collections.Generic;
using NineByteGames.TowerDefense.Behaviors.World;
using NineByteGames.TowerDefense.Buildings;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.Unity;
using UnityEngine;

namespace NineByteGames.TowerDefense.World
{
  /// <summary> Contains world-wide data. </summary>
  internal class WorldScript : AttachedBehavior, IWorld
  {
    [Tooltip("Determines if objects are in the way of placable objects")]
    public PhysicsDetector m_PlaceableObjectChecker;

    public static IWorld World { get; private set; }

    [UnityMethod]
    public void Start()
    {
      var grid = new WorldGrid();
      GridUpdate.ResetGraph();

      Placer = new BuildingWorldPlacement(grid, m_PlaceableObjectChecker);

      World = this;
    }

    /// <inheritdoc/>
    public BuildingWorldPlacement Placer { get; private set; }
  }
}