using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Utils
{
  /// <summary> A coroutine that can be stopped. </summary>
  public struct AdvancedCoroutine
  {
    private readonly MonoBehaviour _owner;
    private readonly IEnumerator _coroutine;

    /// <summary> An empty coroutine. </summary>
    public static readonly AdvancedCoroutine Empty = new AdvancedCoroutine(null, null);

    /// <summary> Constructor. </summary>
    /// <param name="owner"> The owner of the coroutine. </param>
    /// <param name="coroutine"> The coroutine. </param>
    public AdvancedCoroutine(MonoBehaviour owner, IEnumerator coroutine)
    {
      _owner = owner;
      _coroutine = coroutine;
    }

    /// <summary> True if this is a valid coroutine. </summary>
    public bool IsValid
    {
      get { return _owner != null; }
    }

    /// <summary> Starts the execution of this coroutine. </summary>
    public void Start()
    {
      _owner.StartCoroutine(_coroutine);
    }

    /// <summary> Stops the execution of this coroutine. </summary>
    public void Stop()
    {
      _owner.StopCoroutine(_coroutine);
    }

    /// <summary> Returns an object that indicates that a coroutine should wait for the designated amount of time. </summary>
    /// <param name="timeToWait"> The amount of time to wait. </param>
    /// <returns> An object that indicates that a coroutine should wait for the designated amount of time. </returns>
    public static object Wait(TimeSpan timeToWait)
    {
      return new WaitForSeconds((float)timeToWait.TotalSeconds);
    }
  }

  /// <summary> Helps to yield for a coroutine that should only be executed on a specific timer. </summary>
  public class CoroutineTimer
  {
    private readonly float _rechargeRate;
    private float _lastTime;

    public CoroutineTimer(TimeSpan rate)
    {
      _rechargeRate = (float)rate.TotalSeconds;
      _lastTime = 0;
    }

    public object WaitForNext()
    {
      // we want to schedule the next update to occur 1 period from the last update in the future
      var wait = new WaitForSeconds(Mathf.Abs(_lastTime + _rechargeRate - Time.time));
      _lastTime = Time.time;
      return wait;
    }
  }
}