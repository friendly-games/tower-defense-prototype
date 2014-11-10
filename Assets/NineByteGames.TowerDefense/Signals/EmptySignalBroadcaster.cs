using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> A broadcaster to user when a GameObject does contain a broadcaster. </summary>
  internal class EmptySignalBroadcaster : ISignalBroadcaster
  {
    public static readonly ISignalBroadcaster Instance = new EmptySignalBroadcaster();

    private EmptySignalBroadcaster()
    {
    }

    public bool Send<T>(T data)
    {
      return false;
    }

    public void Register(ISignalListener childBehavior)
    {
      throw new NotImplementedException();
    }

    public void Deregister(ISignalListener childBehavior)
    {
      throw new NotImplementedException();
    }
  }
}