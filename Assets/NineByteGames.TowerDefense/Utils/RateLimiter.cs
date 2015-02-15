using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Utils
{
  /// <summary> Limits the rate at which something can occur. </summary>
  public class RateLimiter
  {
    private readonly float _rechargeRate;
    private float _lastTime;

    /// <summary> Constructor. </summary>
    /// <param name="rechargeRate"> The time it takes for an action to "recharge". </param>
    public RateLimiter(TimeSpan rechargeRate)
    {
      _rechargeRate = (float) rechargeRate.TotalSeconds;
      _lastTime = 0;
    }

    /// <summary> True if the item can be re-triggered. </summary>
    public bool CanTrigger
    {
      get { return (Time.time - _lastTime) > _rechargeRate; }
    }

    /// <summary> Attempts to trigger the item. </summary>
    public void Trigger()
    {
      _lastTime = Time.time;
    }
  }
}