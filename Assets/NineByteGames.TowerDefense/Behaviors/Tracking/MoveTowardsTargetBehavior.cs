using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors.Tracking
{
  /// <summary> Move towards the current object. </summary>
  internal class MoveTowardsTargetBehavior : ChildBehavior
  {
    private EntityTrackerBehavior _tracker;

    public override void Start()
    {
      base.Start();

      _tracker = GetComponent<EntityTrackerBehavior>();
    }

    public void Update()
    {
      var target = _tracker.Target;
      if (target == null)
        return;

      rigidbody2D.AddRelativeForce(Vector2.up * rigidbody2D.mass, ForceMode2D.Force);
    }
  }
}