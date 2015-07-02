using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary>
  ///  An object that can send signals out to other classes and can register receivers to listen for
  ///  messages.
  /// </summary>
  public interface ISignalBroadcaster : ISignalSender
  {
    /// <summary>
    ///  Registers a listener to be notified when signals are sent to this broadcaster.
    /// </summary>
    /// <param name="listener"> The listener to register. </param>
    void Register(ISignalReceiver listener);

    /// <summary>
    ///  Unregisters a listener to be notified when signals are sent to this broadcaster.
    /// </summary>
    /// <param name="listener"> The listener to register. </param>
    void Deregister(ISignalReceiver listener);
  }
}