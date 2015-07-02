using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> Callback to be used when responding to signals with data. </summary>
  /// <typeparam name="TData"> The type of data that the signal provides. </typeparam>
  /// <param name="listener"> The listener that shall receive the data. </param>
  /// <param name="data"> The data for the signal. </param>
  public delegate void BehaviorCallback<TData>(ISignalReceiver listener, TData data);
}