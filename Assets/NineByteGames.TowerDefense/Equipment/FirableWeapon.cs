using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Behaviors;
using NineByteGames.TowerDefense.Player;
using NineByteGames.TowerDefense.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace NineByteGames.TowerDefense.Equipment
{
  /// <summary> A weapon that can be fired. </summary>
  public class FirableWeapon : ScriptableObject
  {
    #region Unity Properties

    [Tooltip("The name of the weapon")]
    public string Name;

    [TextArea(1, 3)]
    [Tooltip("Human readable description of the weapon")]
    public string Description;

    [Tooltip("The game object that serves as the weapon")]
    public GameObject WeaponObject;

    [Tooltip("The object to generate when a bullet is fired")]
    public GameObject BulletProjectile;

    [Tooltip("The qualities associated with the weapon")]
    public FirableWeaponQualities Quality;

    #endregion

    /// <summary> Creates an instance of the given firable weapon. </summary>
    /// <returns> The new instance. </returns>
    public FireableWeaponInstance CreateObjectInstance(GameObject owner, LocationAndRotation locationAndRotation)
    {
      var clonedWeapon = WeaponObject.Clone();
      // make sure it's placed under the owner object a little bit to the right
      // TODO should we allow left/right placement
      clonedWeapon.SetParent(owner);
      clonedWeapon.transform.localPosition = locationAndRotation.Location;
      clonedWeapon.transform.localRotation = locationAndRotation.Rotation;

      var weapon = clonedWeapon.GetComponent<WeaponBehavior>();
      weapon.Initialize(this.Quality);
      return new FireableWeaponInstance(clonedWeapon, weapon);
    }
  }

  public class FireableWeaponInstance
  {
    public readonly GameObject Owner;
    public readonly WeaponBehavior Weapon;

    public FireableWeaponInstance(GameObject owner, WeaponBehavior weapon)
    {
      Owner = owner;
      Weapon = weapon;
    }
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