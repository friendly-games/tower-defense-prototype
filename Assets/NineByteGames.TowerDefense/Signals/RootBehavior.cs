using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> Represents an object which is considered "top-level" and can handle signals. </summary>
  [SelectionBase]
  public class RootBehavior : AttachedBehavior, IChildBehavior
  {
    private SignalBroadcaster _signalBroadcaster;

    public void Awake()
    {
      _signalBroadcaster = new SignalBroadcaster();
    }

    public ISignalBroadcaster Broadcaster
    {
      get { return _signalBroadcaster; }
    }
  }
}