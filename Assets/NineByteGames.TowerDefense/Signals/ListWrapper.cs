using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> Base class for generic ListWrapper{T}. </summary>
  public abstract class ListWrapper
  {
    /// <summary> Inserts an item of the required type into the list. </summary>
    /// <param name="signalCallback"> The signal callback, of type TData. </param>
    /// <param name="priority"> The priority of the signal callback. </param>
    /// <param name="instanceIndex"> Zero-based index of the instance. </param>
    public abstract void Insert(object signalCallback, int priority, int instanceIndex);

    /// <summary> Creates a clone of this wrapper. </summary>
    /// <returns> A copy of this object. </returns>
    public abstract ListWrapper Clone();
  }

  /// <summary> A list of signal listeners for a specific type of signal. </summary>
  /// <typeparam name="TData"> The data type associated with the signal. </typeparam>
  public class ListWrapper<TData> : ListWrapper
  {
    private readonly SignalType<TData> _signalType;
    public List<MultiSignalRegistryData> RegisteredListeners { get; private set; }

    /// <summary> Constructor. </summary>
    /// <param name="signalType"> The type of signal that this list holds data for.. </param>
    public ListWrapper(SignalType<TData> signalType)
      : this(signalType, new List<MultiSignalRegistryData>())
    {
    }

    /// <summary> Copy constructor. </summary>
    /// <param name="signalType"> The type of signal that this list holds data for. </param>
    /// <param name="registeredListeners"> The registered listeners. </param>
    private ListWrapper(SignalType<TData> signalType, List<MultiSignalRegistryData> registeredListeners)
    {
      _signalType = signalType;
      RegisteredListeners = registeredListeners;
    }

    /// <inheritdoc />
    public override void Insert(object signalCallback, int priority, int instanceIndex)
    {
      Debug.Log("Actual:" + signalCallback.GetType());
      Debug.Log("Expected:" + typeof(BehaviorCallback<TData>));

      if (signalCallback.GetType() != typeof(BehaviorCallback<TData>))
      {
        Debug.Log("Invalid");
      }

      var newCallback = (BehaviorCallback<TData>)signalCallback;

      int index = 0;

      // find the first instance where an item has a lower priority than the one that we want to
      // insert. 
      while (index < RegisteredListeners.Count && RegisteredListeners[index].Priority >= priority)
      {
        index++;
      }

      var newItem = new MultiSignalRegistryData(priority, newCallback, instanceIndex);

      RegisteredListeners.Insert(index, newItem);
    }

    /// <inheritdoc />
    public override ListWrapper Clone()
    {
      return new ListWrapper<TData>(_signalType, new List<MultiSignalRegistryData>(RegisteredListeners));
    }

    /// <summary>
    ///  Holds a single listener, with a given priority and at which index out of an array that the
    ///  signal listener should be invoked with.
    /// </summary>
    public struct MultiSignalRegistryData
    {
      public readonly int Priority;
      public readonly BehaviorCallback<TData> SignalListener;
      public int InstanceIndex;

      public MultiSignalRegistryData(int priority, BehaviorCallback<TData> signalListener, int instanceIndex)
      {
        Priority = priority;
        SignalListener = signalListener;
        InstanceIndex = instanceIndex;
      }
    }
  }
}