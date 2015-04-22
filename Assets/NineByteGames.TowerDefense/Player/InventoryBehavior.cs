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
    private ITriggerableItem _currentItem;
    private GameObject _previewitem;

    private PlayerCursor _cursor;
    private IInventoryDisplayView _display;

    [UnityMethod]
    public void Start()
    {
      _cursor = GetComponentInChildren<CursorBehavior>().PlayerCursor;
      AttachmentPoints = AttachmentPointsBehavior.RetrieveFor(Owner);

      // TODO use a single inventory list across all items
      _inventoryList = new DataCollection<IInventoryItemBlueprint>(
        Enumerable.Concat<IInventoryItemBlueprint>(weaponList, inventoryList).ToArray()
        );
      _currentItem = _inventoryList.Selected.CreateInstance(this);

      if (_currentItem.PreviewItem != null)
      {
        _previewitem = _currentItem.PreviewItem.Clone();
      }

      // TODO make this not lookup by name
      SetupDisplay();
    }

    /// <inheritdoc />
    public AttachmentToPositionLookup AttachmentPoints { get; private set; }

    /// <inheritdoc />
    public IPlayerCursor Cursor
    {
      get { return _cursor; }
    }

    private void SetupDisplay()
    {
      _display = GameObject.Find("PlayView").GetComponent<IInventoryDisplayView>();

      _display.UpdateSlotText(0, "Pistol");
      _display.UpdateSlotText(1, "Shotgun");
      _display.UpdateSlotText(2, "Wall");
      _display.UpdateSlotText(3, "Wall");
      _display.UpdateSlotText(4, "-");

      _display.SelectedSlot = 1;
    }

    [UnityMethod]
    public void Update()
    {
      if (_previewitem == null)
        return;

      var lowerLeft = GridCoordinate.FromVector3(_cursor.PositionAbsolute);

      //_previewitem.transform.position =
      //  _previewitem.Selected.Strategy.ConvertToGameObjectPosition(lowerLeft);
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

      if (_previewitem != null)
      {
        _previewitem.Kill();
      }

      // TODO do we want to cache this somehow
      if (_currentItem.PreviewItem != null)
      {
        _previewitem = _currentItem.PreviewItem.Clone();
      }

      weaponSwapRate.Restart();
    }

    #region Implementation of IPlayer

    /// <inheritdoc />
    Layer IPlayer.ProjectileLayer
    {
      get { return projectileLayer; }
    }

    #endregion
  }
}