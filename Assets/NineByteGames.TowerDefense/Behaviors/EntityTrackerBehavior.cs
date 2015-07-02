using NineByteGames.TowerDefense.Messages;
using NineByteGames.TowerDefense.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors
{
  /// <summary> Turns the object until it is facing the given target. </summary>
  internal class EntityTrackerBehavior : SignalReceiverBehavior<EntityTrackerBehavior>
  {
    /// <summary> The object that will be tracked. </summary>
    public GameObject Target;

    static EntityTrackerBehavior()
    {
      SignalEntryPoint.For<EntityTrackerBehavior>()
                     .Register(AllSignals.TargetChanged, (i, d) => i.HandleTargetChanged(d));
    }

    private void HandleTargetChanged(TargetAquiredSignal message)
    {
      Target = message.Target;
    }

  }
}