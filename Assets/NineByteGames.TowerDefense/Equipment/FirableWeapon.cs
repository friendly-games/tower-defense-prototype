using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.General;
using NineByteGames.TowerDefense.Player;
using NineByteGames.TowerDefense.Utils;
using NineByteGames.TowerDefense.World;
using UnityEngine;
using UnityEngine.Serialization;

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

    [Tooltip("The qualities associated with the weapon")]
    public FirableWeaponQualities Quality;

    [Tooltip("The projectile for the weapon")]
    public Projectile m_Projectile;

    #endregion

    #region Implementation of IInventoryItemBlueprint

    IInventoryInstance IInventoryItemBlueprint.CreateInstance(IWorld world, IPlayer player)
    {
      player.Inventory.Add(m_Projectile, 10);

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

    /// <inheritdoc/>
    public int MaximumStackAmount
    {
      get { return 1; }
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