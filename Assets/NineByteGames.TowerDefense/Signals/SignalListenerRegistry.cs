using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> Holds a mapping of signal id to callbacks. </summary>
  public class SignalListenerRegistry
  {
    private readonly Dictionary<int, ListWrapper> _callbacks;

    /// <summary> Default constructor. </summary>
    public SignalListenerRegistry()
    {
      _callbacks = new Dictionary<int, ListWrapper>();
    }

    /// <summary> The number of listeners currently registered </summary>
    public int ListenerCount { get; private set; }

    /// <summary> Constructor. </summary>
    /// <param name="oldRegistry"> The registry to copy. </param>
    private SignalListenerRegistry(SignalListenerRegistry oldRegistry)
      : this()
    {
      var newRegistry = this;

      foreach (var kvp in oldRegistry._callbacks)
      {
        newRegistry._callbacks[kvp.Key] = kvp.Value.Clone();
      }

      newRegistry.ListenerCount = oldRegistry.ListenerCount;
    }

    /// <summary> Searches for the wrapper associated with the given signal. </summary>
    /// <typeparam name="TData"> The type of signal to search for. </typeparam>
    /// <param name="signalType"> The data type of the signal. </param>
    /// <returns>
    ///  The wrapper for the given signal type, or null if there is no such signal type registered.
    /// </returns>
    public ListWrapper Find<TData>(SignalType<TData> signalType)
    {
      var id = signalType.UniqueId;

      ListWrapper wrapper;
      if (_callbacks.TryGetValue(id, out wrapper))
      {
        return wrapper;
      }

      return null;
    }

    /// <summary>
    ///  Create a registry that contains all of the listeners registered to this registry plus all of
    ///  the listens included in <paramref name="registry"/>
    /// </summary>
    /// <param name="registry"> The registry from which listeners should be included. </param>
    /// <returns>
    ///  A MultiSignalRegistry that includes the listeners from this registry and the listeners from
    ///  the registry.
    /// </returns>
    public SignalListenerRegistry ClonePlus(TypeSignalRegistry registry)
    {
      var newRegistry = new SignalListenerRegistry(this);

      // add the new signals from the new listener
      foreach (var item in registry.RegisteredSignals)
      {
        newRegistry.Add(item.TypeId, item.Callback, item.Priority);
      }

      // we now have one more listener
      newRegistry.ListenerCount++;

      return newRegistry;
    }

    private void Add(ISignalTypeId signalType, object signalCallback, int priority)
    {
      ListWrapper list;
      if (!_callbacks.TryGetValue(signalType.UniqueId, out list))
      {
        list = signalType.CreateList();
        _callbacks[signalType.UniqueId] = list;
      }

      list.Insert(signalCallback, priority, ListenerCount);
    }
  }
}