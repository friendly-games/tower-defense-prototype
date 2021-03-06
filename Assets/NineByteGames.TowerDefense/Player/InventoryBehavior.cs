﻿using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.Common.Data;
using NineByteGames.TowerDefense.Behaviors.World;
using NineByteGames.TowerDefense.Buildings;
using NineByteGames.TowerDefense.Equipment;
using NineByteGames.TowerDefense.General;
using NineByteGames.TowerDefense.Items;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.UI;
using NineByteGames.TowerDefense.Unity;
using NineByteGames.TowerDefense.Utils;
using NineByteGames.TowerDefense.World;
using NineByteGames.TowerDefense.World.Grid;
using UnityEngine;

namespace NineByteGames.TowerDefense.Player
{
  /// <summary>
  ///  Contains all functionality related to the player and its current inventory.
  /// </summary>
  internal class InventoryBehavior : AttachedBehavior, IPlayer, INotifee
  {
    #region Unity Properties

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

    #endregion

    private DataCollection<IInventoryItemBlueprint> _inventoryList;
    private IInventoryInstance _currentItem;

    private PlayerCursor _cursor;
    private IInventoryDisplayView _display;

    private IMoneyBank _bank;
    private IWorld _world;
    private IPlayerInventory _inventory;

    [UnityMethod]
    public void Start()
    {
      // TODO get the world through some other means (do we really need to?)
      _world = WorldScript.World;

      _inventory = new PlayerInventory();

      _bank = new MoneyBank
      {
        AmountChanged = new Notifee(this, NotificationIds.Money)
      };

      _cursor = GetComponentInChildren<CursorBehavior>().PlayerCursor;
      AttachmentPoints = AttachmentPointsBehavior.RetrieveFor(Owner);

      _inventoryList = new DataCollection<IInventoryItemBlueprint>(
        Enumerable.Concat<IInventoryItemBlueprint>(weaponList, inventoryList).ToArray()
        );
      _currentItem = _inventoryList.Selected.CreateInstance(WorldScript.World, this);

      SetupDisplay();
    }

    private void SetupDisplay()
    {
      // TODO make this not lookup by name
      _display = GameObject.Find("PlayView").GetComponent<IInventoryDisplayView>();

      int index = 0;
      foreach (var blueprint in _inventoryList)
      {
        _display.UpdateSlotText(index, blueprint.Name);
        index++;
      }

      _display.SelectedSlot = 0;
    }

    /// <summary> Activate the primary item, for example, firing a weapon. </summary>
    public void TryTrigger1()
    {
      if (_currentItem.Trigger())
      {
        buildingRate.Restart();
      }
    }

    /// <summary> Activate the secondary item, for example, placing an object. </summary>
    public void TryTrigger2()
    {
      if (!buildingRate.CanTrigger)
        return;

      if (_currentItem.Trigger())
      {
        buildingRate.Restart();
      }
    }

    public void TryReload()
    {
      // TODO should this really be reload?
      _currentItem.Reload();
    }

    /// <summary> Try to switch to the given inventory item. </summary>
    /// <param name="inventoryId"> The inventory item to switch to. </param>
    public void TrySwitchTo(int inventoryId)
    {
      if (!weaponSwapRate.CanTrigger)
        return;

      _display.SelectedSlot = inventoryId;

      if (!_inventoryList.SetSelectedIndex(inventoryId))
        return;

      _currentItem.MarkDone();
      _currentItem = _inventoryList.Selected.CreateInstance(_world, this);

      weaponSwapRate.Restart();
    }

    /// <inheritdoc />
    void INotifee.NotifyChange(int id)
    {
      switch (id)
      {
        case NotificationIds.Money:
          _display.UpdateMoney(_bank.CurrentAmount);
          break;
      }
    }

    #region Implementation of IPlayer

    /// <inheritdoc />
    Layer IPlayer.ProjectileLayer
    {
      get { return projectileLayer; }
    }

    /// <inheritdoc />
    public IPlayerCursor Cursor
    {
      get { return _cursor; }
    }

    public IMoneyBank Bank
    {
      get { return _bank; }
    }

    IPlayerInventory IPlayer.Inventory
    {
      get { return _inventory; }
    }

    /// <inheritdoc />
    public AttachmentToPositionLookup AttachmentPoints { get; private set; }

    #endregion
  }
}