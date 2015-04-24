using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.General;
using NineByteGames.TowerDefense.Player;
using NineByteGames.TowerDefense.Utils;
using UnityEngine;

namespace NineByteGames.TowerDefense.Equipment
{
  /// <summary> A weapon that can be fired. </summary>
  public class FirableWeapon : ScriptableObject, IInventoryItemBlueprint
  {
    #region Unity Properties

    [Tooltip("The name of the weapon")]
    public string Name;

    [TextArea(1, 3)]
    [Tooltip("Human readable description of the weapon")]
    public string Description;

    [Tooltip("The game object that serves as the weapon.  Requires it to have a WeaponBehavior")]
    public GameObject WeaponObject;

    [Tooltip("The object to generate when a bullet is fired")]
    public GameObject BulletProjectile;

    [Tooltip("The qualities associated with the weapon")]
    public FirableWeaponQualities Quality;

    #endregion

    #region Implementation of IInventoryItemBlueprint

    IInventoryInstance IInventoryItemBlueprint.CreateInstance(IPlayer player)
    {
      var locationAndRotation = player.AttachmentPoints[AttachmentPoint.Weapon];

      // TODO should we allow left/right placement
      var clonedWeapon = WeaponObject.Clone(player.Owner, locationAndRotation);
      var weapon = clonedWeapon.GetComponent<WeaponBehavior>();

      weapon.Initialize(this, player);
      return weapon;
    }

    /// <inheritdoc />
    string IInventoryItemBlueprint.Name
    {
      get { return this.Name; }
    }

    #endregion
  }

  [Serializable]
  public class FirableWeaponQualities
  {
    [Tooltip("How fast the weapon can be fired")]
    public TimeField WeaponRechargeRate;

    [Tooltip("The number of projectiles that are fired when the weapon is triggered")]
    [Range(1, 50)]
    public int NumberOfProjectiles = 1;

    [Tooltip("The range (in degrees) that the projectile can be fired.")]
    [Range(0, 30)]
    public float Variability = 1f;
  }
}