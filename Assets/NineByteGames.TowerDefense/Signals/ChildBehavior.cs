using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> A behavior that acts as a child of a RootBehavior.</summary>
  public abstract class ChildBehavior : AttachedBehavior, IChildBehavior, ISignalListener
  {
    /// <summary>
    ///  The behavior through which all messages are sent and listeners are registered for.
    /// </summary>
    public RootBehavior RootBehavior { get; private set; }

    /// <summary> Find the root behavior. </summary>
    public virtual void Start()
    {
      SetupRoot();
    }

    /// <summary> Find the root behavior who "owns" this child. </summary>
    private void SetupRoot()
    {
      RootBehavior = gameObject.RetrieveInHierarchy<RootBehavior>();

      if (RootBehavior == null)
        throw new Exception("No RootBehavior found");
      if (RootBehavior.Broadcaster == null)
        throw new Exception("RootBehavior.Broadcaster is null");

      RootBehavior.Broadcaster.Register(this);
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