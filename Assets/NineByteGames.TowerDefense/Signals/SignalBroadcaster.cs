using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> A concrete implementation of ISignalBroadcaster. </summary>
  public class SignalBroadcaster : ISignalBroadcaster, IDisposable
  {
    private Dictionary<Type, LinkedList<SignalListenerAndPriority>> _listeners;

    /// <summary> Default constructor. </summary>
    public SignalBroadcaster()
    {
      _listeners = new Dictionary<Type, LinkedList<SignalListenerAndPriority>>();
    }

    /// <inheritdoc />
    public void Register(ISignalListener childBehavior)
    {
      var info = ChildListenerRegistry.GetFor(childBehavior.GetType());

      foreach (var entry in info.Entries)
      {
        LinkedList<SignalListenerAndPriority> list;
        // if a list does not already exist
        if (!_listeners.TryGetValue(entry.Key, out list))
        {
          list = new LinkedList<SignalListenerAndPriority>();
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
        LinkedList<SignalListenerAndPriority> list;
        if (!_listeners.TryGetValue(entry.Key, out list))
          continue;

        var node = list.First;

        while (node != null)
        {
          var next = node.Next;
          if (node.Value.SignalListener == childBehavior)
          {
            list.Remove(node);
          }
          node = next;
        }

        if (list.Count == 0)
        {
          _listeners.Remove(entry.Key);
        }
      }
    }

    /// <inheritdoc />
    public bool Send<T>(T data)
    {
      LinkedList<SignalListenerAndPriority> list;
      if (!_listeners.TryGetValue(typeof(T), out list))
        return false;

      using (var options = SignalListenerContext.Push())
      {
        for (var node = list.First;
             node != null && options.ShouldContinue;
             node = node.Next)
        {
          SignalListenerAndPriority child = node.Value;
          ((ISignalListener<T>)child.SignalListener).Handle(data);
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
    private static void InsertIntoSortedList(LinkedList<SignalListenerAndPriority> list,
                                             SignalListenerAndPriority newEntry)
    {
      int currentPriority = newEntry.Priority;

      var head = list.First;

      while (head != null && head.Value.Priority >= currentPriority)
      {
        head = head.Next;
      }

      if (head == null)
      {
        list.AddLast(newEntry);
      }
      else
      {
        list.AddBefore(head, newEntry);
      }
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