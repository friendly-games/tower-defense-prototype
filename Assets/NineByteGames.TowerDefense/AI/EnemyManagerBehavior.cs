using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Signals;

namespace NineByteGames.TowerDefense.AI
{
  /// <summary> Manages all of the enemies that currently exist in the world. </summary>
  internal class EnemyManagerBehavior : AttachedBehavior
  {
    private EnemyManager _manager;

    public void Start()
    {
      _manager = new EnemyManager(Owner);
    }

    /// <summary> The actual enemy manager. </summary>
    public EnemyManager InstanceManager
    {
      get { return _manager; }
    }
  }
}