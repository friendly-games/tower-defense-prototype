using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Behaviors;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace NineByteGames.TowerDefense.Equipment
{
  /// <summary> Controls how a weapon is fired. </summary>
  public class WeaponBehavior : AttachedBehavior
  {
    #region Unity Properties

    [Tooltip("The object to generate when a bullet is fired")]
    [FormerlySerializedAs("bulletProjectile")]
    public GameObject BulletProjectile;

    #endregion

    private FirableWeaponQualities _qualities;
    private RateLimiter _rechargeRate;

    /// <summary> Initialize the object with required fields. </summary>
    /// <param name="qualities"> The qualities of the weapon. </param>
    public void Initialize(FirableWeaponQualities qualities)
    {
      _qualities = qualities;
      _rechargeRate = qualities.WeaponRechargeRate.ToRateLimiter();
    }

    /// <summary> Attempt to fire the weapon. </summary>
    /// <param name="positionAndDirection"> The location from which the projectile should be created
    ///  and the direction the projectile should travel. </param>
    /// <param name="layer"> The layer on which the projectile should target. </param>
    public bool AttemptFire(Ray positionAndDirection, Layer layer)
    {
      if (!_rechargeRate.CanTrigger)
        return false;

      _rechargeRate.Restart();

      for (int i = 0; i < _qualities.NumberOfProjectiles; i++)
      {
        InitializeProjectile(BulletProjectile.Clone(), _qualities, layer, positionAndDirection);
      }

      return true;
    }

    /// <summary>
    ///  Initialize the projectile to begin moving forward from the given <paramref name="transform"/>,
    ///  on the given <paramref name="layer"/>.  Add variability in the direction based on the
    ///  <paramref name="qualities"/> passed in.
    /// </summary>
    private static void InitializeProjectile(GameObject projectileClone,
                                             FirableWeaponQualities qualities,
                                             Layer layer,
                                             Ray positionAndDirection)
    {
      // TODO should we attach to something else
      var projectileBehavior = projectileClone.GetComponent<ProjectileBehavior>();
      var cloneTransform = projectileClone.GetComponent<Transform>();
      var bodyTransform = projectileClone.GetComponent<Rigidbody2D>();

      // we want to tilt it based on the Variability
      var randomness = Random.Range(-qualities.Variability, qualities.Variability);
      var velocity = Quaternion.AngleAxis(randomness, Vector3.forward) * positionAndDirection.direction;

      bodyTransform.velocity = velocity * projectileBehavior.InitialSpeed;
      cloneTransform.position = positionAndDirection.origin + positionAndDirection.direction;
      projectileClone.layer = layer.LayerId;
    }
  }
}