using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Behaviors;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> A behavior that contains a reference to a RootBehavior.</summary>
  public interface IChildBehavior
  {
    RootBehavior RootBehavior { get; }
  }
}