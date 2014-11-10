using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Messages;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.Utils;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors
{
  /// <summary> Turns the object until it is facing the given target. </summary>
  internal class EntityTrackerBehavior : ChildBehavior, ISignalListener<TargetAquiredSignal>
  {
    /// <summary> The object that will be tracked. </summary>
    public GameObject Target;

    /// <summary> The speed at which the object should rotate towards its target. </summary>
    public float RotationSpeed = 1.0f;

    /// <inheritdoc />
    public void FixedUpdate()
    {
      if (Target == null)
        return;

      MathUtils.RotateTowards(Owner, Target, Time.deltaTime * RotationSpeed);
    }

    bool ISignalListener<TargetAquiredSignal>.Handle(TargetAquiredSignal message)
    {
      Target = message.Target;
      return false;
    }
  }
}