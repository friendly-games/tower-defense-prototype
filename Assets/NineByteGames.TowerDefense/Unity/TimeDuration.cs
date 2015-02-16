using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine;

namespace NineByteGames.TowerDefense.Unity
{
  /// <summary> Holds a TimeSpan that can be serialized by unity. </summary>
  [Serializable]
  [StructLayout(LayoutKind.Explicit)]
  public struct TimeDuration
  {
    [SerializeField]
    [FieldOffset(0)]
    [UsedImplicitly]
    private int ticks1;

    [SerializeField]
    [FieldOffset(4)]
    [UsedImplicitly]
    private int ticks2;

    [FieldOffset(0)]
    [UsedImplicitly]
    private TimeSpan timeSpan;

    public TimeDuration(int ticks1, int ticks2)
    {
      this.timeSpan = new TimeSpan();
      this.ticks1 = ticks1;
      this.ticks2 = ticks2;
    }

    public static implicit operator TimeSpan(TimeDuration timeDuration)
    {
      return timeDuration.timeSpan;
    }

    /// <summary> Convert a timespan into the two ticks values. </summary>
    /// <remarks>
    ///  Required as unity cannot serialize a TimeSpan directly, so for the editor, we need to edit
    ///  the ticks1 and ticks2 fields.
    /// </remarks>
    /// <param name="timespan"> The timespan to convert. </param>
    /// <param name="ticks1"> [out] The first ticks. </param>
    /// <param name="ticks2"> [out] The second ticks. </param>
    public static void Convert(TimeSpan timespan, out int ticks1, out int ticks2)
    {
      var time = new TimeDuration {timeSpan = timespan};

      ticks1 = time.ticks1;
      ticks2 = time.ticks2;
    }
  }
}