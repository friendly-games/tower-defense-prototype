using System;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;

namespace NineByteGames.TowerDefense.Utils
{
  /// <summary> An object that can notify others when it is destroyed. </summary>
  internal interface INotifyOnDestruction
  {
    /// <summary> Raised when the item is destroyed. </summary>
    event Action<Object> Destroyed;
  }
}