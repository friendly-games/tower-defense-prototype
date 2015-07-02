using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Behaviors.Data;
using UnityEngine;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> Represents an object which is considered "top-level" and can handle signals. </summary>
  [SelectionBase]
  public class RootBehavior : AttachedBehavior, IChildBehavior
  {
    private ISignalBroadcaster _signalBroadcaster;
    private Dictionary<object, SharedValue> _sharedValues;

    public void Awake()
    {
      _sharedValues = new Dictionary<object, SharedValue>();
      _signalBroadcaster = SignalEntryPoint.CreateBroadcaster();
    }

    public ISignalBroadcaster Broadcaster
    {
      get { return _signalBroadcaster; }
    }

    /// <summary> Retrieves the shared data by name. </summary>
    /// <typeparam name="T"> Generic type parameter. </typeparam>
    /// <param name="key"> The name of the shared data to retrieve. </param>
    /// <param name="sharedValue"> The value to retrieve. </param>
    public void Retrieve<T>(string key, out SharedValue<T> sharedValue)
    {
      SharedValue value;
      if (_sharedValues.TryGetValue(key, out value))
      {
        sharedValue = (SharedValue<T>)value;
      }
      else
      {
        sharedValue = new SharedValue<T>();
        _sharedValues.Add(key, sharedValue);
      }
    }
  }
}