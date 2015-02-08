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
    private EntityTrackerBehavior _tracking;

    public void Start()
    {
      _tracking = GetComponent<EntityTrackerBehavior>();
    }

    /// <summary> The speed at which the object should move towards its target. </summary>
    public float Speed = 1.0f;

    /// <inheritdoc />
    public void FixedUpdate()
    {
      if (_tracking.Target == null)
        return;

      MathUtils.RotateTowards(Owner, _tracking.Target, Time.deltaTime * Speed);
    }
  }
}