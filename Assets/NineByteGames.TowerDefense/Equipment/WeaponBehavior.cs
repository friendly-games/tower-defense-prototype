using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Behaviors;
using NineByteGames.TowerDefense.General;
using NineByteGames.TowerDefense.Player;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace NineByteGames.TowerDefense.Equipment
{
  /// <summary> Controls how a weapon is fired. </summary>
  internal class WeaponBehavior : AttachedBehavior, IInventoryInstance
  {
    #region Unity Properties

    [Tooltip("The object to generate when a bullet is fired")]
    [FormerlySerializedAs("bulletProjectile")]
    public GameObject BulletProjectile;

    #endregion

    private FirableWeaponQualities _qualities;
    private RateLimiter _rechargeRate;
    private FirableWeapon _blueprint;
    private IPlayer _player;

    /// <summary> Initialize the object with required fields. </summary>
    public void Initialize(FirableWeapon blueprint, IPlayer player)
    {
      _blueprint = blueprint;
      _qualities = blueprint.Quality;
      _player = player;
      _rechargeRate = _qualities.WeaponRechargeRate.ToRateLimiter();
    }

    #region Implementation of IInventoryInstance

    public string Name
    {
      get { return _blueprint.Name; }
    }

    public bool Trigger()
    {
      if (!_rechargeRate.CanTrigger)
        return false;

      var currentTransform = Owner.GetComponent<Transform>();

      // we want the projectile to move towards the current target (as opposed to directly straight
      // out of the muzzle).  While this is less "correct" it should lead to a better player
      // experience. 
      var direction = _player.Cursor.PositionAbsolute - currentTransform.position;
      var positionAndDirection = new Ray(currentTransform.position, direction.normalized);

      // TODO layer
      _rechargeRate.Restart();

      for (int i = 0; i < _qualities.NumberOfProjectiles; i++)
      {
        InitializeProjectile(BulletProjectile.Clone(), _qualities, _player.ProjectileLayer, positionAndDirection);
      }

      return true;
    }

    public void MarkDone()
    {
      Owner.Kill();
    }

    #endregion

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