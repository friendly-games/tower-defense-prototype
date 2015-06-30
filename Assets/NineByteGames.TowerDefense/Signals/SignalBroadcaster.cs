using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> A concrete implementation of ISignalBroadcaster. </summary>
  public class SignalBroadcaster : ISignalBroadcaster, IDisposable
  {
    private Dictionary<Type, List<SignalListenerAndPriority>> _listeners;

    /// <summary> Default constructor. </summary>
    public SignalBroadcaster()
    {
      _listeners = new Dictionary<Type, List<SignalListenerAndPriority>>();
    }

    /// <inheritdoc />
    public void Register(ISignalListener childBehavior)
    {
      var info = ChildListenerRegistry.GetFor(childBehavior.GetType());

      foreach (var entry in info.Entries)
      {
        List<SignalListenerAndPriority> list;
        // if a list does not already exist
        if (!_listeners.TryGetValue(entry.Key, out list))
        {
          list = new List<SignalListenerAndPriority>();
          _listeners[entry.Key] = list;
        }

        // insert it so that high priority listeners come first
        InsertIntoSortedList(list, new SignalListenerAndPriority(entry.Value, childBehavior));
      }
    }

    /// <inheritdoc />
    public void Deregister(ISignalListener childBehavior)
    {
      if (_listeners == null)
        return;

      var info = ChildListenerRegistry.GetFor(childBehavior.GetType());

      foreach (var entry in info.Entries)
      {
        List<SignalListenerAndPriority> list;
        if (!_listeners.TryGetValue(entry.Key, out list))
          continue;

        list.RemoveAll(slap => slap.SignalListener == childBehavior);

        if (list.Count == 0)
        {
          _listeners.Remove(entry.Key);
        }
      }
    }

    /// <inheritdoc />
    public bool Send<T>(T data)
    {
      List<SignalListenerAndPriority> list;
      if (!_listeners.TryGetValue(typeof(T), out list))
        return false;

      foreach (SignalListenerAndPriority child in list)
      {
        var result = ((ISignalListener<T>)child.SignalListener).Handle(data);

        if (result == SignalListenerResult.StopProcessing)
        {
          // stop processing then
          return true;
        }
      }

      return true;
    }

    public void Dispose()
    {
      if (_listeners == null)
        return;

      _listeners.Clear();
      _listeners = null;
    }

    /// <summary> Inserts a new signal into the priority list (that is already sorted). </summary>
    private static void InsertIntoSortedList(List<SignalListenerAndPriority> list, SignalListenerAndPriority newEntry)
    {
      int insertIndex = 0;
      int currentPriority = newEntry.Priority;

      while (insertIndex < list.Count && list[insertIndex].Priority > currentPriority)
      {
        insertIndex++;
      }

      list.Insert(insertIndex, newEntry);
    }

    private struct SignalListenerAndPriority
    {
      public readonly int Priority;
      public readonly ISignalListener SignalListener;

      public SignalListenerAndPriority(int priority, ISignalListener signalListener)
      {
        Priority = priority;
        SignalListener = signalListener;
      }
    }
  }
}