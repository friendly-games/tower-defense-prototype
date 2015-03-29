using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Behaviors;
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

    public void AttemptTrigger(Transform transform, Layer layer)
    {
      if (!Quality.weaponRechargeRate.CanTrigger)
        return;

      // TODO we shouldn't affect this objects weapon recharge rate
      Quality.weaponRechargeRate.Restart();

      for (int i = 0; i < Quality.NumberOfProjectiles; i++)
      {
        InitializeProjectile(BulletProjectile.Clone(), Quality, transform, layer);
      }
    }

    /// <summary>
    ///  Initialize the projectile to begin moving forward from the given <paramref name="transform"/>,
    ///  on the given <paramref name="layer"/>.  Add variability in the direction based on the
    ///  <paramref name="qualities"/> passed in.
    /// </summary>
    private static void InitializeProjectile(GameObject projectileClone,
                                             FirableWeaponQualities qualities,
                                             Transform transform,
                                             Layer layer)
    {
      // TODO should we attach to something else
      var projectileBehavior = projectileClone.GetComponent<ProjectileBehavior>();
      var cloneTransform = projectileClone.GetComponent<Transform>();
      var bodyTransform = projectileClone.GetComponent<Rigidbody2D>();

      // we want to tilt it based on the Variability
      var randomness = Random.Range(-qualities.Variability, qualities.Variability);
      var velocity = Quaternion.AngleAxis(randomness, Vector3.forward) * transform.up;

      bodyTransform.velocity = velocity * projectileBehavior.InitialSpeed;
      cloneTransform.position = transform.position + transform.up;
      projectileClone.layer = layer.LayerId;
    }
  }

  [Serializable]
  public class FirableWeaponQualities
  {
    [Tooltip("How fast the weapon can be fired")]
    public RateLimiter weaponRechargeRate;

    [Tooltip("The number of projectiles that are fired when the weapon is triggered")]
    [Range(1, 50)]
    public int NumberOfProjectiles = 1;

    [Tooltip("The range (in degrees) that the projectile can be fired.")]
    [Range(0, 30)]
    public float Variability = 1f;
  }
}