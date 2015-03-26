using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.Common.Extensions;
using NineByteGames.TowerDefense.Behaviors;
using NineByteGames.TowerDefense.Equipment;
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
    public PlaceableObject[] inventoryList;

    [Tooltip("List of weapons currently in inventory")]
    public FirableWeapon[] weaponList;

    [Tooltip("The layer on which projectiles should be created")]
    public Layer projectileLayer;

    [Tooltip("How fast weapons can be switched")]
    public RateLimiter weaponSwapRate;

    [Tooltip("The rate at which buildings can be placed")]
    public RateLimiter buildingRate;

    /// <summary> The current inventory item index. </summary>
    private int _currentInventoryItemIndex;

    private Vector3 _cursorLocation;

    private GameObject _fake;

    public void Start()
    {
      _currentInventoryItemIndex = 0;

      _fake = Placeable.PreviewItem.Clone();
    }

    public PlaceableObject Placeable
    {
      get { return inventoryList[_currentInventoryItemIndex]; }
    }

    public FirableWeapon Weapon
    {
      get { return weaponList[0]; }
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
      Weapon.AttemptTrigger(Owner.GetComponent<Transform>(), projectileLayer);
    }

    /// <summary> Activate the secondary item, for example, placing an object. </summary>
    public void TryTrigger2()
    {
      if (!buildingRate.CanTrigger)
        return;

      buildingRate.Restart();

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
      if (!weaponSwapRate.CanTrigger
          || _currentInventoryItemIndex == inventoryId
          || !inventoryList.IsIndexValid(inventoryId))
        return;

      _currentInventoryItemIndex = inventoryId;
      // TODO do we want to cache this somehow
      _fake.DestorySelf();
      _fake = Placeable.PreviewItem.Clone();
      weaponSwapRate.Restart();
    }
  }
}