using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> A behavior that contains a reference to a RootBehavior.</summary>
  public interface IChildBehavior
  {
    /// <summary> The signal broadcaster associated with this child-behavior. </summary>
    ISignalBroadcaster Broadcaster { get; }
  }
}