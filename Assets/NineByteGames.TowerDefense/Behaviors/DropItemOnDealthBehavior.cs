using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Messages;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NineByteGames.TowerDefense.Behaviors
{
  public class DropItemOnDealthBehavior : ChildBehavior, ISignalListener<SignalIndicators.DeathIndicator>
  {
    public GameObject ItemToDropOnDeath;

    [SignalPriority(SignalPriorities.High)]
    SignalListenerResult ISignalListener<SignalIndicators.DeathIndicator>.Handle(SignalIndicators.DeathIndicator health)
    {
      const float range = 0.5f;
      var offset = new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range));
      var cloned = ItemToDropOnDeath.Clone(transform.position + offset, transform.rotation);
      cloned.SetParent(GameObject.Find("Dropped Items"));

      return SignalListenerResult.Continue;
    }
  }
}