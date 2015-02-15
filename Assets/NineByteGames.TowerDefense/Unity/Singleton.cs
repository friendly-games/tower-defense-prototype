using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Signals;

namespace NineByteGames.TowerDefense.Unity
{
  /// <summary> An object that acts as a singleton. </summary>
  internal class Singleton<T> : AttachedBehavior
    where T : Singleton<T>
  {
    /// <summary> Singleton instance of the given class. </summary>
    public static T Instance { get; private set; }
  }
}