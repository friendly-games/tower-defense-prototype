using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.Utils;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors.Tracking
{
  [RequireComponent(typeof(EntityTrackerBehavior))]
  internal class RotateTowardsTargetBehavior : AttachedBehavior
  {
    [Tooltip("The target to move towards")]
    public GameObject Target;

    /// <summary> The speed at which the object should move towards its target. </summary>
    public float Speed = 1.0f;

    /// <inheritdoc />
    public void FixedUpdate()
    {
      if (Target == null)
        return;

      MathUtils.RotateTowards(Owner, Target, Time.deltaTime * Speed);
    }
  }
}