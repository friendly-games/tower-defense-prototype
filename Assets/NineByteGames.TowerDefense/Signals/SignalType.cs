using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> Represents a unique category of signal that can be sent to listeners. </summary>
  /// <typeparam name="TData"> The type of data to be sent to lisetners. </typeparam>
  public class SignalType<TData> : ISignalTypeId
  {
    /// <summary> The unique id for the signal. </summary>
    private readonly int _uniqueId;

    /// <summary> Default constructor. </summary>
    public SignalType()
    {
      _uniqueId = SignalTypeBase.Next();
    }

    /// <summary> The unique id for the signal. </summary>
    public int UniqueId
    {
      get { return this._uniqueId; }
    }

    /// <inheritdoc />
    ListWrapper ISignalTypeId.CreateList()
    {
      return new ListWrapper<TData>(this);
    }
  }

  /// <summary> A signal which sends no data to callees. </summary>
  public class SignalType : SignalType<EmptyArg>
  {
  }
}