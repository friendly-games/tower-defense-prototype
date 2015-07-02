using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> Manages signal listeners and sending messages to those signal listeners. </summary>
  public class SignalBroadcaster : ISignalBroadcaster
  {
    /// <summary>
    ///  A list that can be used to cache old listeners so that we don't have to allocate a new list
    ///  for deregistration.
    /// </summary>
    private static readonly List<ISignalReceiver> CachedListeners = new List<ISignalReceiver>();

    /// <summary> The current node that contains the signals that can be handled. </summary>
    private SignalRegistryTree.SignalNode _node;

    /// <summary> The receivers that should be invoked for the signal receivers. </summary> 
    private readonly List<ISignalReceiver> _sources;

    /// <summary> True if a removal needs to be processed. </summary>
    private bool _isPendingRemoval;

    /// <summary> Constructor. </summary>
    /// <param name="tree"> The tree for which this signal broadcaster is valid. </param>
    public SignalBroadcaster(SignalRegistryTree tree)
    {
      _node = tree.Root;
      _sources = new List<ISignalReceiver>();
    }

    /// <inheritdoc />
    public bool Send<TData>(SignalType<TData> signalType, TData data)
    {
      RemovePendingRemovals();

      var wrapper = (ListWrapper<TData>)_node.Registry.Find(signalType);
      if (wrapper == null)
        return false;

      using (var options = SignalListenerContext.Push())
      {
        for (int index = 0;
             options.ShouldContinue && index < wrapper.RegisteredListeners.Count;
             index++)
        {
          var item = wrapper.RegisteredListeners[index];
          var instance = _sources[item.InstanceIndex];
          item.SignalListener(instance, data);
        }
      }

      return true;
    }

    /// <inheritdoc />
    public bool Send(SignalType signalType)
    {
      RemovePendingRemovals();

      return Send<EmptyArg>(signalType, null);
    }

    /// <inheritdoc />
    public bool Supports<TData>(SignalType<TData> signalType)
    {
      RemovePendingRemovals();

      return _node.Registry.Find(signalType) != null;
    }

    /// <inheritdoc />
    public void Register(ISignalReceiver listener)
    {
      RemovePendingRemovals();

      _sources.Add(listener);
      _node = _node.GetOrCreateChildNodeFor(listener.RegisteredTypeId);

      if (_node.Registry.ListenerCount != _sources.Count)
      {
        Debug.Log("Something when wrong");
      }

      System.Diagnostics.Debug.Assert(_node.Registry.ListenerCount == _sources.Count);
    }

    /// <inheritdoc />
    public void Deregister(ISignalReceiver listener)
    {
      _isPendingRemoval = true;

      // null out all of the listeners
      for (int i = 0; i < CachedListeners.Count; i++)
      {
        var existing = CachedListeners[i];

        if (existing == listener)
        {
          CachedListeners[i] = null;
        }
      }
    }

    /// <summary> Removes listeners that were marked for removal. </summary>
    private void RemovePendingRemovals()
    {
      if (!_isPendingRemoval)
        return;

      _isPendingRemoval = false;

      CachedListeners.Clear();
      CachedListeners.AddRange(_sources);

      _node = _node.GetRootNode();
      _sources.Clear();

      // re-register of the old registers that isn't the one that we're looking for
      foreach (var oldListener in CachedListeners)
      {
        if (oldListener != null)
        {
          Register(oldListener);
        }
      }

    }

    /// <inheritdoc />
    public void Dispose()
    {
      _sources.Clear();
      _node = null;
    }
  }
}