using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> An object that can send signals out to other classes. </summary>
  public interface ISignalSender
  {
    /// <summary> Returns true if the given signal has registered listeners. </summary>
    /// <param name="signalType"> The signal type to check. </param>
    /// <returns> True if listeners are registered, false otherwise. </returns>
    bool Supports<TData>(SignalType<TData> signalType);

    /// <summary> Send a signal to all registered signal listeners. </summary>
    /// <typeparam name="TData"> The type of data the signal sends. </typeparam>
    /// <param name="signalType"> The signal to send. </param>
    /// <param name="data"> The data associated with the signal. </param>
    bool Send<TData>(SignalType<TData> signalType, TData data);

    /// <summary> Send a signal to all registered signal listeners. </summary>
    /// <param name="signalType"> The signal to send. </param>
    bool Send(SignalType signalType);
  }
}