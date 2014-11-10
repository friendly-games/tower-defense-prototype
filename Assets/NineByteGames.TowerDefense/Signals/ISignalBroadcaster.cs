using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> An object that can send signals out to other classes. </summary>
  public interface ISignalBroadcaster
  {
    /// <summary> Sends the message to all listeners of the broadcaster. </summary>
    /// <typeparam name="T"> The type of message to send. </typeparam>
    /// <param name="data"> The data associated with the message. </param>
    /// <returns> True if the message was handled, false otherwise. </returns>
    bool Send<T>(T data);

    /// <summary> Registers a listener to be able to receive events sent to this broadcaster.  </summary>
    /// <param name="childBehavior"> The listener to register. </param>
    void Register(ISignalListener childBehavior);

    /// <summary> Un-registers a listener so that it no longer receives events sent to this broadcaster.  </summary>
    /// <param name="childBehavior"> The child listener to remove. </param>
    void Deregister(ISignalListener childBehavior);
  }
}