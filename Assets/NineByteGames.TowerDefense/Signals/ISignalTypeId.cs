using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> Non-generic interface that all signals must implement. </summary>
  public interface ISignalTypeId
  {
    /// <summary> The unique id for the signal. </summary>
    int UniqueId { get; }

    /// <summary> A list of CallbackAndPriority{TData} for the specific type of signal. </summary>
    /// <returns> The new list. </returns>
    ListWrapper CreateList();
  }
}