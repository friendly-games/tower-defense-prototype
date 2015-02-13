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
    [Tooltip("The item that will be placed in the world")]
    public PlaceableObject Placeable;

    private RateLimiter _weaponRecharge;
    private GameObject _fake;

    /// <summary> The layer on which projectiles should be created. </summary>
    public Layer ProjectileLayer;

    /// <summary> The object to generate when a bullet is fired. </summary>
    public GameObject BulletProjectile;

    private Vector3 _cursorLocation;

    public void Start()
    {
      _weaponRecharge = new RateLimiter(TimeSpan.FromSeconds(0.5f));

      _fake = Placeable.PreviewItem.Clone();
    }

    /// <summary> Updates the current location of the cursor. </summary>
    /// <param name="location"> The newest location of the cursor. </param>
    public void UpdateCursor(Vector3 location)
    {
      _cursorLocation = location;
      var lowerLeft = GridCoordinate.FromVector3(_cursorLocation);

      _fake.GetComponent<Transform>().position = Placeable.ConvertToGameObjectPosition(lowerLeft);
    }

    /// <summary> True if we can activate the primary item. </summary>
    public bool CanTrigger1
    {
      get { return _weaponRecharge.Trigger(); }
    }

    /// <summary> Activate the primary item, for example, firing a weapon. </summary>
    public void Trigger1()
    {
      BulletProjectile.GetComponent<ProjectileBehavior>()
                      .CreateAndInitializeFrom(Owner.transform, ProjectileLayer);
    }

    /// <summary> True if we can activate the secondary item. </summary>
    public bool CanTrigger2
    {
      get { return _weaponRecharge.Trigger(); }
    }

    /// <summary> Activate the secondary item, for example, placing an object. </summary>
    public void Trigger2()
    {
      var lowerLeft = GridCoordinate.FromVector3(_cursorLocation);
      Managers.Towers.PlaceAt(lowerLeft);
    }
  }
}