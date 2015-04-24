using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Buildings;
using NineByteGames.TowerDefense.Equipment;
using NineByteGames.TowerDefense.General;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.UI;
using NineByteGames.TowerDefense.Unity;
using NineByteGames.TowerDefense.Utils;
using NineByteGames.TowerDefense.World.Grid;
using UnityEngine;

namespace NineByteGames.TowerDefense.Player
{
  /// <summary>
  ///  Contains all functionality related to the player and its current inventory.
  /// </summary>
  internal class InventoryBehavior : AttachedBehavior, IPlayer
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

    [UnityMethod]
    public void Start()
    {
      _cursor = GetComponentInChildren<CursorBehavior>().PlayerCursor;
      AttachmentPoints = AttachmentPointsBehavior.RetrieveFor(Owner);

      _inventoryList = new DataCollection<IInventoryItemBlueprint>(
        Enumerable.Concat<IInventoryItemBlueprint>(weaponList, inventoryList).ToArray()
        );
      _currentItem = _inventoryList.Selected.CreateInstance(this);

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
      _currentItem = _inventoryList.Selected.CreateInstance(this);

      weaponSwapRate.Restart();
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

    /// <inheritdoc />
    public AttachmentToPositionLookup AttachmentPoints { get; private set; }

    #endregion
  }
}