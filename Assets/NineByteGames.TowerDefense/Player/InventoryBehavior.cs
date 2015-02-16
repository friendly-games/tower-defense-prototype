using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.Common.Extensions;
using NineByteGames.TowerDefense.Behaviors;
using NineByteGames.TowerDefense.Objects;
using NineByteGames.TowerDefense.Services;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.Utils;
using NineByteGames.TowerDefense.World.Grid;
using UnityEngine;

namespace NineByteGames.TowerDefense.Player
{
  /// <summary>
  ///  Contains all functionality related to the player and its current inventory.
  /// </summary>
  internal class InventoryBehavior : AttachedBehavior
  {
    [Tooltip("List of items currently in inventory")]
    public PlaceableObject[] InventoryList;

    /// <summary> The layer on which projectiles should be created. </summary>
    public Layer ProjectileLayer;

    /// <summary> The object to generate when a bullet is fired. </summary>
    public GameObject BulletProjectile;

    /// <summary> The current inventory item index. </summary>
    private int _currentInventoryItemIndex;

    private Vector3 _cursorLocation;

    private RateLimiter _weaponRechargeLimiter;
    private RateLimiter _weaponSwapLimiter;
    private RateLimiter _buildingPlacer;

    private GameObject _fake;

    public void Start()
    {
      _weaponRechargeLimiter = new RateLimiter(TimeSpan.FromSeconds(0.5f));
      _weaponSwapLimiter = new RateLimiter(TimeSpan.FromMilliseconds(500));
      _buildingPlacer = new RateLimiter(TimeSpan.FromMilliseconds(100));

      _currentInventoryItemIndex = 0;

      _fake = Placeable.PreviewItem.Clone();
    }

    public PlaceableObject Placeable
    {
      get { return InventoryList[_currentInventoryItemIndex]; }
    }

    /// <summary> Updates the current location of the cursor. </summary>
    /// <param name="location"> The newest location of the cursor. </param>
    public void UpdateCursor(Vector3 location)
    {
      _cursorLocation = location;
      var lowerLeft = GridCoordinate.FromVector3(_cursorLocation);

      _fake.GetComponent<Transform>().position = Placeable.Strategy.ConvertToGameObjectPosition(lowerLeft);
    }

    /// <summary> Activate the primary item, for example, firing a weapon. </summary>
    public void TryTrigger1()
    {
      if (!_weaponRechargeLimiter.CanTrigger)
        return;

      _weaponRechargeLimiter.Restart();

      BulletProjectile.GetComponent<ProjectileBehavior>()
                      .CreateAndInitializeFrom(Owner.transform, ProjectileLayer);
    }

    /// <summary> Activate the secondary item, for example, placing an object. </summary>
    public void TryTrigger2()
    {
      if (!_buildingPlacer.CanTrigger)
        return;

      _buildingPlacer.Restart();

      var lowerLeft = GridCoordinate.FromVector3(_cursorLocation);

      if (Managers.Placer.CanCreate(lowerLeft, Placeable))
      {
        Managers.Placer.PlaceAt(lowerLeft, Placeable);
      }
    }

    /// <summary> Try to switch to the given inventory item. </summary>
    /// <param name="inventoryId"> The inventory item to switch to. </param>
    public void TrySwitchTo(int inventoryId)
    {
      if (!_weaponSwapLimiter.CanTrigger
          || _currentInventoryItemIndex == inventoryId
          || !InventoryList.IsValid(inventoryId))
        return;

      _currentInventoryItemIndex = inventoryId;
      // TODO do we want to cache this somehow
      _fake.DestorySelf();
      _fake = Placeable.PreviewItem.Clone();
      _weaponSwapLimiter.Restart();
    }
  }
}