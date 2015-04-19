using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NineByteGames.TowerDefense.Utils
{
  /// <summary>
  ///  Maintains a list of other components and manages ending the tracking if the tracked items are
  ///  destroyed.
  /// </summary>
  /// <typeparam name="T"> The type of item to keep track of. </typeparam>
  internal sealed class ObjectTracker<T> : IDisposable, IEnumerable<T>
    where T : Component, INotifyOnDestruction
  {
    private readonly AttachedBehavior _owner;
    private readonly HashSet<T> _currentlyTracking;

    /// <summary> Constructor. </summary>
    /// <param name="owner"> The owner of the list of tracked items. </param>
    public ObjectTracker(AttachedBehavior owner)
    {
      if (owner == null)
        throw new ArgumentNullException("owner");

      _owner = owner;
      _currentlyTracking = new HashSet<T>();
    }

    /// <summary> Constructor. </summary>
    /// <param name="owner"> The owner of the list of tracked items. </param>
    /// <param name="changedDelegate"> The delegate to subscribe to the Changed event. </param>
    public ObjectTracker(AttachedBehavior owner, Action changedDelegate)
      : this(owner)
    {
      if (changedDelegate == null)
        throw new ArgumentNullException("changedDelegate");

      Changed += changedDelegate;
    }

    /// <summary>
    ///  Invoked when a new item is tracked or an existing item is no longer tracked.
    /// </summary>
    public event Action Changed;

    private void OnChanged()
    {
      var handler = Changed;
      if (handler != null)
        handler();
    }

    /// <summary> Starts tracking <paramref name="item"/>. </summary>
    /// <param name="item"> The item to track. </param>
    public void StartTracking(T item)
    {
      if (_currentlyTracking.Add(item))
      {
        item.Destroyed += HandleDestroyed;
        OnChanged();
      }
    }

    /// <summary> Stops tracking the <paramref name="item"/> </summary>
    /// <param name="item"> The item to cease tracking. </param>
    public void StopTracking(T item)
    {
      // we're about to die and unsubscribe everyone anyways, so no need to remove it here. 
      if (_owner.IsDead)
        return;

      if (_currentlyTracking.Remove(item))
      {
        item.Destroyed -= HandleDestroyed;
        OnChanged();
      }
    }

    private void HandleDestroyed(Object behavior)
    {
      StopTracking((T)behavior);
    }

    /// <summary> Stops tracking all items. </summary>
    public void Dispose()
    {
      foreach (var tracking in _currentlyTracking)
      {
        tracking.Destroyed -= HandleDestroyed;
      }

      _currentlyTracking.Clear();
    }

    /// <summary> The current number of tracked items.  </summary>
    public int Count
    {
      get { return _currentlyTracking.Count; }
    }

    /// <inheritdoc />
    public IEnumerator<T> GetEnumerator()
    {
      return _currentlyTracking.GetEnumerator();
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}