using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> Holds all of the signals that will be used with a single type. </summary>
  public class TypeSignalRegistry
  {
    /// <summary> Constructor. </summary>
    /// <param name="typeId"> The type id of the signal for which the listeners should be registered. </param>
    public TypeSignalRegistry(TypeId typeId)
    {
      Id = typeId;
      RegisteredSignals = new List<SignalCallback>();
    }

    /// <summary> The id of the type for which the registry is valid. </summary>
    public TypeId Id { get; private set; }

    /// <summary> All of the signals that have been registered with the registry. </summary>
    public List<SignalCallback> RegisteredSignals { get; private set; }

    /// <summary> Registers the callback for the current type. </summary>
    /// <typeparam name="TData"> The type of data that the signal provides. </typeparam>
    /// <param name="typeId"> The type id of the signal for which the listener should be registered. </param>
    /// <param name="callback"> The callback to invoke when the signal fires. </param>
    /// <param name="priority"> The priority of the callback in relation to all callbacks. </param>
    public void Register<TData>(ISignalTypeId typeId,
                                BehaviorCallback<TData> callback,
                                int priority)
    {
      var cap = new SignalCallback(callback, typeId, priority);

      RegisteredSignals.Add(cap);
    }

    /// <summary> Holds data about a registered callback. </summary>
    public class SignalCallback
    {
      /// <summary> The id of the signal for which the callback was created. </summary>
      public readonly ISignalTypeId TypeId;

      /// <summary> The callback. </summary>
      public readonly object Callback;

      /// <summary> The priority of the callback. </summary>
      public readonly int Priority;

      /// <summary> Constructor. </summary>
      /// <param name="callback"> The callback. </param>
      /// <param name="typeId"> The id of the signal for which the callback was created. </param>
      /// <param name="priority"> The priority of the callback. </param>
      public SignalCallback(object callback, ISignalTypeId typeId, int priority)
      {
        Callback = callback;
        TypeId = typeId;
        Priority = priority;
      }
    }
  }
}