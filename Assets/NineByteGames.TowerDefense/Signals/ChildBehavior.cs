using NineByteGames.TowerDefense.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> A behavior that acts as a child of a RootBehavior.</summary>
  public abstract class ChildBehavior : AttachedBehavior, IChildBehavior, ISignalListener
  {
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

    /// <inheritdoc />
    public ISignalBroadcaster Broadcaster { get; private set; }

    /// <summary>
    ///  Finds the ultimate parent of this object that contains the RootBehavior for this hiearachy.
    /// </summary>
    /// <returns> The found root parent. </returns>
    public GameObject FindRootParent()
    {
      return FindRootBehavior().gameObject;
    }

    /// <summary> Finds the parent of this object that contains the RootBeavior for this hiearachy. </summary>
    private RootBehavior FindRootBehavior()
    {
      var rootBehavior = gameObject.RetrieveInHierarchy<RootBehavior>();

      if (rootBehavior == null)
        throw new Exception("No RootBehavior found");
      if (rootBehavior.Broadcaster == null)
        throw new Exception("RootBehavior.Broadcaster is null");

      return rootBehavior;
    }

    /// <summary> Sends the message to all listeners of the broadcaster. </summary>
    /// <typeparam name="T"> The type of message to send. </typeparam>
    /// <param name="data"> The data associated with the message. </param>
    /// <returns> True if the message was handled, false otherwise. </returns>
    protected bool Send<T>(T data)
    {
      return Broadcaster.Send(data);
    }
  }
}