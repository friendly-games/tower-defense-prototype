using NineByteGames.TowerDefense.Unity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary>
  ///  Allows a class to be registered so that it can receive signals sent to the root of the object.
  /// </summary>
  /// <typeparam name="T"> Generic type parameter. </typeparam>
  public abstract class SignalReceiverBehavior<T> : ChildBehavior, ISignalReceiver, IChildBehavior
    where T : SignalReceiverBehavior<T>
  {
    /// <inheritdoc />
    public ISignalBroadcaster Broadcaster { get; private set; }

    /// <inheritdoc />
    TypeId ISignalReceiver.RegisteredTypeId
    {
      get { return SignalEntryPoint.GetIdFor(GetType()); }
    }

    /// <inheritdoc />
    bool ISignalReceiver.Enabled
    {
      get { return this.enabled; }
    }

    /// <summary> Find the root behavior. </summary>
    [UnityMethod]
    public virtual void Start()
    {
      Broadcaster = FindRootBehavior().Broadcaster;
      Broadcaster.Register(this);
    }

    [UnityMethod]
    public virtual void OnDestroy()
    {
      if (Broadcaster != null)
      {
        Broadcaster.Deregister(this);
      }
    }
  }
}