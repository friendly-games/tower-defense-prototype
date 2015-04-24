using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Behaviors;
using NineByteGames.TowerDefense.General;
using NineByteGames.TowerDefense.Player;
using NineByteGames.TowerDefense.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

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

      var clonedWeapon = WeaponObject.Clone();
      // make sure it's placed under the owner object a little bit to the right
      // TODO should we allow left/right placement
      clonedWeapon.SetParent(player.Owner);
      clonedWeapon.transform.localPosition = locationAndRotation.Location;
      clonedWeapon.transform.localRotation = locationAndRotation.Rotation;

      var weapon = clonedWeapon.GetComponent<WeaponBehavior>();
      weapon.Initialize(this.Quality);
      return new FireableWeaponInstance(player, clonedWeapon, weapon);
    }

    /// <inheritdoc />
    string IInventoryItemBlueprint.Name
    {
      get { return this.Name; }
    }

    #endregion
  }

  internal class FireableWeaponInstance : IInventoryInstance
  {
    private readonly IPlayer _player;
    public readonly GameObject Owner;
    public readonly WeaponBehavior Weapon;

    public FireableWeaponInstance(IPlayer player, GameObject owner, WeaponBehavior weapon)
    {
      _player = player;
      Owner = owner;
      Weapon = weapon;
    }

    #region Implementation of ITriggerableItem

    /// <inheritdoc />
    string IInventoryInstance.Name
    {
      get { return Weapon.name; }
    }

    /// <inheritdoc />
    bool IInventoryInstance.Trigger()
    {
      var currentTransform = Owner.GetComponent<Transform>();

      // we want the projectile to move towards the current target (as opposed to directly straight
      // out of the muzzle).  While this is less "correct" it should lead to a better player
      // experience. 
      var direction = _player.Cursor.PositionAbsolute - currentTransform.position;
      var positionAndDirection = new Ray(currentTransform.position, direction.normalized);

      // TODO layer
      return Weapon.AttemptFire(positionAndDirection, Layer.FromName("Projectile (Player)"));
    }

    /// <inheritdoc />
    void IInventoryInstance.MarkDone()
    {
      Owner.Kill();
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