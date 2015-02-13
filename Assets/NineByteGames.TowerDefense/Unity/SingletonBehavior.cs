using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Signals;

namespace NineByteGames.TowerDefense.Unity
{
  internal class SingletonBehavior<T> : AttachedBehavior
    where T : SingletonBehavior<T>
  {
    public static T Instance { get; private set; }

    public void Start()
    {
      Instance = (T) this;
    }
  }
}