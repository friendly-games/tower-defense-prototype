using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace NineByteGames.TowerDefense.Utils
{
  /// <summary> Wrapper around timespan. </summary>
  [Serializable]
  public struct TimeField
  {
    [SerializeField]
    private float _timeValue;

    /// <summary> Constructor. </summary>
    /// <param name="timespan"> The length of time that the field represents. </param>
    public TimeField(TimeSpan timespan)
    {
      _timeValue = (float)timespan.TotalMilliseconds;
    }

    /// <summary> The rate at which the limiter expires and can be used again. </summary>
    public TimeSpan Time
    {
      get { return TimeSpan.FromMilliseconds(_timeValue); }
      set { _timeValue = (float)value.TotalMilliseconds; }
    }

    /// <summary> Create a RateLimiter based on the amount of time in this object. </summary>
    /// <returns> A rate limiter that lasts as long as this time field. </returns>
    public RateLimiter ToRateLimiter()
    {
      return new RateLimiter(Time);
    }
  }
}