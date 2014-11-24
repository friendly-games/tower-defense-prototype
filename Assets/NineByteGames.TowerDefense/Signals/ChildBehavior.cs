using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Behaviors;
using UnityEngine;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> A behavior that acts as a child of a RootBehavior.</summary>
  public abstract class ChildBehavior : AttachedBehavior, IChildBehavior, ISignalListener
  {
    public RootBehavior RootBehavior { get; private set; }

    private void SetupRoot()
    {
      RootBehavior = gameObject.RetrieveInHierarchy<RootBehavior>();

      if (RootBehavior == null)
        throw new Exception("No RootBehavior found");
      if (RootBehavior.Broadcaster == null)
        throw new Exception("RootBehavior.Broadcaster is null");

      RootBehavior.Broadcaster.Register(this);
    }

    public override void Start()
    {
      base.Start();
      SetupRoot();
    }

    public void OnDestroy()
    {
      RemoveRoot();
    }

    /// <summary> Sends the message to all listeners of the broadcaster. </summary>
    /// <typeparam name="T"> The type of message to send. </typeparam>
    /// <param name="data"> The data associated with the message. </param>
    /// <returns> True if the message was handled, false otherwise. </returns>
    protected bool Send<T>(T data)
    {
      return RootBehavior.Broadcaster.Send(data);
    }

    private void RemoveRoot()
    {
      if (RootBehavior != null)
      {
        RootBehavior.Broadcaster.Deregister(this);
      }
    }
  }
}