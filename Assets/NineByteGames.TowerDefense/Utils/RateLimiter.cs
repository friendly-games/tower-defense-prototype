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

    /// <summary> Attempts to trigger the item. </summary>
    /// <returns> true if the item was triggered, false if it is still in the cooldown time. </returns>
    public bool Trigger()
    {
      if (Time.time - _lastTime > _rechargeRate)
      {
        _lastTime = Time.time;
        return true;
      }

      return false;
    }
  }
}