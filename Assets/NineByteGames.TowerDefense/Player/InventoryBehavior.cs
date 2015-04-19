using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.Common.Extensions;
using NineByteGames.TowerDefense.Behaviors;
using NineByteGames.TowerDefense.Buildings;
using NineByteGames.TowerDefense.Equipment;
using NineByteGames.TowerDefense.Services;
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
  internal class InventoryBehavior : AttachedBehavior
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

    private DataCollection<PlaceableObject> _placeables;
    private DataCollection<FirableWeapon> _weapons;
    private GameObject _placeablePreviewItem;
    private FireableWeaponInstance _currentWeapon;
    private AttachmentToPositionLookup _lookup;
    private PlayerCursor _cursor;
    private IInventoryDisplayView _display;

    [UnityMethod]
    public void Start()
    {
      _placeables = new DataCollection<PlaceableObject>(inventoryList);
      _weapons = new DataCollection<FirableWeapon>(weaponList);
      _cursor = GetComponentInChildren<CursorBehavior>().PlayerCursor;

      _lookup = AttachmentPointsBehavior.RetrieveFor(Owner);

      _placeablePreviewItem = _placeables.Selected.PreviewItem.Clone();
      _currentWeapon = _weapons.Selected.CreateObjectInstance(Owner, _lookup[AttachmentPoint.Weapon]);

      // TODO make this not lookup by name
      SetupDisplay();
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
      var lowerLeft = GridCoordinate.FromVector3(_cursor.CursorPositionAbsolute);

      _placeablePreviewItem.transform.position =
        _placeables.Selected.Strategy.ConvertToGameObjectPosition(lowerLeft);
    }

    /// <summary> Activate the primary item, for example, firing a weapon. </summary>
    public void TryTrigger1()
    {
      var currentTransform = Owner.GetComponent<Transform>();

      // we want the projectile to move towards the current target (as opposed to directly straight
      // out of the muzzle).  While this is less "correct" it should lead to a better player
      // experience. 
      var direction = _cursor.CursorPositionAbsolute - currentTransform.position;
      var positionAndDirection = new Ray(currentTransform.position, direction.normalized);

      _currentWeapon.Weapon.AttemptFire(positionAndDirection, projectileLayer);
    }

    /// <summary> Activate the secondary item, for example, placing an object. </summary>
    public void TryTrigger2()
    {
      if (!buildingRate.CanTrigger)
        return;

      buildingRate.Restart();

      var lowerLeft = GridCoordinate.FromVector3(_cursor.CursorPositionAbsolute);

      if (Managers.Placer.CanCreate(lowerLeft, _placeables.Selected))
      {
        Managers.Placer.PlaceAt(lowerLeft, _placeables.Selected);
      }
    }

    /// <summary> Try to switch to the given inventory item. </summary>
    /// <param name="inventoryId"> The inventory item to switch to. </param>
    public void TrySwitchTo(int inventoryId)
    {
      if (!weaponSwapRate.CanTrigger)
        return;

      _display.SelectedSlot = inventoryId;

      SwitchPlaceable(inventoryId);
      SwitchWeapon(inventoryId);

      weaponSwapRate.Restart();
    }

    private void SwitchPlaceable(int inventoryId)
    {
      if (!_placeables.SetSelectedIndex(inventoryId))
        return;

      // TODO do we want to cache this somehow
      _placeablePreviewItem.Kill();
      _placeablePreviewItem = _placeables.Selected.PreviewItem.Clone();
    }

    private void SwitchWeapon(int inventoryId)
    {
      if (!_weapons.SetSelectedIndex(inventoryId))
        return;

      // TODO implement switching weapons animation
      _currentWeapon.Owner.Kill();
      _currentWeapon = _weapons.Selected.CreateObjectInstance(Owner, _lookup[AttachmentPoint.Weapon]);
    }
  }
}