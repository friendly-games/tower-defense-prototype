using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Behaviors.Tracking;
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

    public virtual void Start()
    {

    }

    public void DestroyOwner()
    {
      Destroy(Owner);
    }

    public void DestroyOwner(float secondsToLive)
    {
      Destroy(Owner, secondsToLive);
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