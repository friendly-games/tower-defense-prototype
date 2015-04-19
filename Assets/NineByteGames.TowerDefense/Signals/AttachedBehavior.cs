using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Utils;
using UnityEngine;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> A component that is attached to and stays attached to a single game object. </summary>
  public abstract class AttachedBehavior : MonoBehaviour
  {
    /// <summary> The owner of the component. </summary>
    public GameObject Owner
    {
      get { return gameObject; }
    }

    /// <summary> Gets a value indicating whether this object is alive. </summary>
    public bool IsAlive
    {
      get { return this.IsAlive(); }
    }

    /// <summary> Gets a value indicating whether this object is dead. </summary>
    public bool IsDead
    {
      get { return this.IsDead(); }
    }

    public void DestroyOwner()
    {
      Destroy(Owner);
    }

    public void DestroyOwner(float secondsToLive)
    {
      Destroy(Owner, secondsToLive);
    }

    /// <summary>
    ///  Gets a component of type T or adds a new component of type T if the component does not
    ///  already exist on the owner.
    /// </summary>
    /// <typeparam name="T"> Generic type parameter. </typeparam>
    /// <returns> The existing or newly created instance of T. </returns>
    public T GetOrAddComponent<T>()
      where T : Component
    {
      return GetComponent<T>()
             ?? Owner.AddComponent<T>();
    }

    /// <summary> Creates a coroutine and starts it. </summary>
    /// <param name="coroutine"> The coroutine to create a wrapper for. </param>
    /// <returns> The new coroutine. </returns>
    public AdvancedCoroutine CreateCoroutine(IEnumerator coroutine)
    {
      var createdCoroutine = new AdvancedCoroutine(this, coroutine);
      createdCoroutine.Start();
      return createdCoroutine;
    }
  }
}