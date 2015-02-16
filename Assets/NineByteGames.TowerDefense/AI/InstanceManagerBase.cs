using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Behaviors;
using NineByteGames.TowerDefense.Objects;
using UnityEngine;

namespace NineByteGames.TowerDefense.AI
{
  /// <summary>
  ///  Represents a concrete implementation of IInstanceManager. Tracks the lifetime of various game
  ///  objects, remembering alive objects and removing dead objects.
  /// </summary>
  internal class InstanceManagerBase : IInstanceManager
  {
    /// <summary> Constructor. </summary>
    /// <param name="parent"> The parent to assign to newly created instances. </param>
    public InstanceManagerBase(GameObject parent)
    {
      Parent = parent;
      Instances = new HashSet<GameObject>();
    }

    /// <summary> The enemies that currently exist in the world.. </summary>
    protected HashSet<GameObject> Instances { get; private set; }

    /// <summary> The parent to assign to newly created instances. </summary>
    protected GameObject Parent { get; private set; }

    /// <summary> Adds the object to this manager to be tracked. </summary>
    public void NotifyAlive(GameObject instance)
    {
      Instances.Add(instance);
    }

    /// <summary> Removes this object from the manager to be tracked. </summary>
    public void NotifyDestroy(GameObject instance)
    {
      Instances.Remove(instance);
    }

    /// <summary> Creates a new instance of the given prefab. </summary>
    /// <param name="prefab"> The prefab to create an instance of. </param>
    /// <param name="position"> The location at which the new instance should be placed. </param>
    /// <param name="euler"> The rotation at which the new instance should be placed. </param>
    /// <returns> A GameObject representing the newly created instance. </returns>
    protected GameObject Create(GameObject prefab, Vector3 position, Quaternion euler)
    {
      var cloned = prefab.Clone(position, euler);
      cloned.SetParent(Parent);

      var prefabBased = cloned.AddComponent<LifetimeManagementBehavior>();
      prefabBased.Manager = this;

      return cloned;
    }
  }
}