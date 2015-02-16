using System;
using System.Collections.Generic;
using System.Linq;
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

    private RateLimiter _weaponRecharge;

    private GameObject _fake;

    public void Start()
    {
      _weaponRecharge = new RateLimiter(TimeSpan.FromSeconds(0.5f));
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

    /// <summary> True if we can activate the primary item. </summary>
    public bool CanTrigger1
    {
      get { return _weaponRecharge.CanTrigger; }
    }

    /// <summary> Activate the primary item, for example, firing a weapon. </summary>
    public void Trigger1()
    {
      _weaponRecharge.Trigger();

      BulletProjectile.GetComponent<ProjectileBehavior>()
                      .CreateAndInitializeFrom(Owner.transform, ProjectileLayer);
    }

    /// <summary> True if we can activate the secondary item. </summary>
    public bool CanTrigger2
    {
      get { return _weaponRecharge.CanTrigger; }
    }

    /// <summary> Activate the secondary item, for example, placing an object. </summary>
    public void Trigger2()
    {
      _weaponRecharge.Trigger();

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
      if (_currentInventoryItemIndex != inventoryId && inventoryId < InventoryList.Length)
      {
        _currentInventoryItemIndex = inventoryId;
        // TODO do we want to cache this somehow
        _fake.DestorySelf();
        _fake = Placeable.PreviewItem.Clone();
      }
    }
  }
}