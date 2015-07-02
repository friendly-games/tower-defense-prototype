using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> Non generic SignalType. </summary>
  public static class SignalTypeBase
  {
    private static int _counter = 0;

    /// <summary> Increments the static counter for all signal types. </summary>
    /// <returns> An integer representing the signal type. </returns>
    public static int Next()
    {
      return _counter++;
    }

    /// <summary> Resets the SignalType back to zero. </summary>
    public static void Reset()
    {
      _counter = 0;
    }
  }
}