using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> A list of pre-defined priorities. </summary>
  public static class SignalPriorities
  {
    public const int VeryLow = -10000;
    public const int Low = -100;
    public const int Normal = 0;
    public const int High = 100;
    public const int VeryHigh = 10000;
  }
}