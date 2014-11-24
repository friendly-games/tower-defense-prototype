using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Messages;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors
{
  /// <summary> Turns the object until it is facing the given target. </summary>
  internal class EntityTrackerBehavior : ChildBehavior, ISignalListener<TargetAquiredSignal>
  {
    /// <summary> The object that will be tracked. </summary>
    public GameObject Target;

    bool ISignalListener<TargetAquiredSignal>.Handle(TargetAquiredSignal message)
    {
      Target = message.Target;
      return false;
    }
  }
}