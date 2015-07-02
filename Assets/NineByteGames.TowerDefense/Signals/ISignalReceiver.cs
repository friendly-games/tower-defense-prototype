using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> A class that can register to receive signals. </summary>
  public interface ISignalReceiver
  {
    /// <summary> The type id associated with the receiver. </summary>
    TypeId RegisteredTypeId { get; }

    /// <summary> True if the receiver is enabled.  Currently not used. </summary>
    bool Enabled { get; }
  }
}