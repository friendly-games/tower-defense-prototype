using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.AI;
using NineByteGames.TowerDefense.Signals;

namespace NineByteGames.TowerDefense.Behaviors
{
  /// <summary> Represents an object that was created via a prefab. </summary>
  internal class PrefabBasedObjectBehavior : AttachedBehavior
  {
    public IInstanceManager Manager { get; set; }

    public void Start()
    {
      Manager.NotifyAlive(Owner);
    }

    public void OnDestroy()
    {
      Manager.NotifyDestroy(Owner);
    }
  }
}