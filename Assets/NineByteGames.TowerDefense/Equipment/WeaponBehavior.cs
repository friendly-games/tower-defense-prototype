using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Behaviors;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.Utils;
using UnityEngine;

namespace NineByteGames.TowerDefense.Equipment
{
  /// <summary> Controls how a weapon is fired. </summary>
  public class WeaponBehavior : AttachedBehavior
  {
    [Tooltip("How fast the weapon can be fired")]
    public RateLimiter weaponRechargeRate;

    [Tooltip("The object to generate when a bullet is fired")]
    public GameObject bulletProjectile;

    [Tooltip("The number of projectiles that are fired when the weapon is triggered")]
    [Range(1, 50)]
    public int NumberOfProjectiles = 1;

    [Tooltip("The range (in degrees) that the projectile can be fired.")]
    [Range(0, 30)]
    public float Variability = 1f;

    /// <summary> Fires the weapon. </summary>
    /// <param name="layer"> The layer on which projectiles should fire. </param>
    public void Fire(Layer layer)
    {
      if (!weaponRechargeRate.CanTrigger)
        return;

      weaponRechargeRate.Restart();

      bulletProjectile.GetComponent<ProjectileBehavior>()
                      .CreateAndInitializeFrom(Owner.transform, layer);
    }
  }
}