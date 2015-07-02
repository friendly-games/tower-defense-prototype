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

    public bool Supports<TData>(SignalType<TData> signalType)
    {
      return false;
    }

    bool ISignalSender.Send<TData>(SignalType<TData> signalType, TData data)
    {
      return false;
    }

    bool ISignalSender.Send(SignalType signalType)
    {
      return false;
    }

    void ISignalBroadcaster.Register(ISignalReceiver listener)
    {
      throw new NotImplementedException();
    }

    void ISignalBroadcaster.Deregister(ISignalReceiver listener)
    {
      throw new NotImplementedException();
    }
  }
}