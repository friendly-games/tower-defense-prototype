using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Messages;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.Unity;
using NineByteGames.TowerDefense.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace NineByteGames.TowerDefense.Behaviors
{
  /// <summary> Add damage to the objects that trigger the collider on this object. </summary>
  [RequireComponent(typeof(Collider2D))]
  internal class DamageOnTriggerBehavior : AttachedBehavior
  {
    [Tooltip("The layers on which to cause damage")]
    public LayerMask layerMask;

    [Tooltip("The amount of damage to do to touched entities")]
    public float damage;

    [Tooltip("How fast damage occurs on a given target")]
    [FormerlySerializedAs("attackLimiter")]
    public RateLimiter rateOfAttack;

    public void OnTriggerStay2D(Collider2D other)
    {
      if (!rateOfAttack.CanTrigger)
        return;

      var target = other.gameObject;
      if (!layerMask.Contains(target))
        return;

      target.SendSignal(new Damage(this.damage));
      rateOfAttack.Restart();
    }
  }
}