using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Messages
{
  /// <summary> Indicates that an object has died or been destroyed. </summary>
  public struct DeathIndicator
  {
    public static readonly DeathIndicator Signal = new DeathIndicator();
  }
}