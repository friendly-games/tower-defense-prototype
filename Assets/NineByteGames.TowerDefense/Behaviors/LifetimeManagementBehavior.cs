using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.AI;
using NineByteGames.TowerDefense.Signals;

namespace NineByteGames.TowerDefense.Behaviors
{
  /// <summary> Represents an object that was created via a prefab. </summary>
  internal class LifetimeManagementBehavior : AttachedBehavior
  {
    /// <summary> The manager to notify when the object becomes alive or dies. </summary>
    public IInstanceManager Manager { get; set; }

    /// <summary> Some sort of data associated with the object. </summary>
    public object Tag { get; set; }

    /// <summary> Gets the tag data typed to T </summary>
    public T GetTag<T>()
    {
      return (T)Tag;
    }

    public void Start()
    {
      Manager.NotifyAlive(Owner);
    }

    public void OnDestroy()
    {
      Manager.NotifyDestroy(Owner);
    }
  }
}