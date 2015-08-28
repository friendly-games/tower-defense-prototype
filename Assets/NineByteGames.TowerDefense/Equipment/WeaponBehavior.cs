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
    [Tooltip("The relative position at which a bullet should be fired")]
    public GameObject m_BulletStartPoint;

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

    /// <summary> Fire a single projectile in the direction indicated. </summary>
    /// <param name="positionAndDirection"> The position and direction at which the projectile should be fired. </param>
    private void FireProjectile(Ray positionAndDirection)
    {
      GameObject projectileClone = _blueprint.m_Projectile.m_Prefab.Clone();
      Layer layer = _player.ProjectileLayer;

      // TODO should we attach to something else
      var projectileBehavior = projectileClone.GetComponent<ProjectileBehavior>();
      var cloneTransform = projectileClone.GetComponent<Transform>();
      var bodyTransform = projectileClone.GetComponent<Rigidbody2D>();

      // we want to tilt it based on the Variability
      var randomness = Random.Range(-_qualities.Variability, _qualities.Variability);
      var velocity = Quaternion.AngleAxis(randomness, Vector3.forward) * positionAndDirection.direction;

      bodyTransform.velocity = velocity * projectileBehavior.InitialSpeed;
      cloneTransform.position = m_BulletStartPoint.transform.position;
      projectileClone.layer = layer.LayerId;
    }

    #region Implementation of IInventoryInstance

    /// <inheritdoc />
    public string Name
    {
      get { return _blueprint.Name; }
    }

    /// <inheritdoc />
    public bool Trigger()
    {
      if (!_rechargeRate.CanTrigger)
        return false;

      int count = _player.Inventory.GetCountOf(_blueprint.m_Projectile);
      if (count <= 0)
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
        FireProjectile(positionAndDirection);
      }

      _player.Inventory.Remove(_blueprint.m_Projectile, 1);

      return true;
    }

    /// <inheritdoc />
    public void MarkDone()
    {
      Owner.Kill();
    }

    #endregion
  }
}