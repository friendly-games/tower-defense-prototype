using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.AI;
using NineByteGames.TowerDefense.Behaviors.World;
using NineByteGames.TowerDefense.World;
using NineByteGames.TowerDefense.World.Grid;
using UnityEngine;

namespace NineByteGames.TowerDefense.Towers
{
  /// <summary> Manages the creation of towers. </summary>
  internal class TowerManager : InstanceManagerBase
  {
    private readonly WorldGrid _worldGrid;
    private readonly GameObject _towerPrefab;
    private readonly HashSet<GameObject> _towers;

    public TowerManager(GameObject parent, WorldGrid worldGrid, GameObject towerPrefab)
      : base(parent)
    {
      _worldGrid = worldGrid;
      _towerPrefab = towerPrefab;
      _towers = new HashSet<GameObject>();
    }

    /// <summary> Returns true if a tower can be placed at the designated location. </summary>
    public bool CanCreate(GridCoordinate location)
    {
      return _worldGrid.IsEmpty(location, new Size(2, 2));
    }

    /// <summary> Places a tower at the designated location. </summary>
    /// <param name="lowerLeft"> The lower left location of the tower. </param>
    public void PlaceAt(GridCoordinate lowerLeft)
    {
      var position = GetCenterOf2x2(lowerLeft);
      var clone = Create(_towerPrefab, position, Quaternion.identity);

      GridUpdate.MarkWalkable(lowerLeft, new Size(2, 2), false);
    }

    /// <summary>
    ///  Gets the unity position of a building with a 2x2 size whose lower left position is given by
    ///  <paramref name="lowerLeft"/>
    /// </summary>
    private static Vector3 GetCenterOf2x2(GridCoordinate lowerLeft)
    {
      var location = lowerLeft.ToVector3();
      location.x += 0.5f;
      location.y += 0.5f;
      return location;
    }
  }
}