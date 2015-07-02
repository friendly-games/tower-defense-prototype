using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Utils;

namespace NineByteGames.TowerDefense.Messages
{
  /// <summary> Represents the amount of damage something inflicts. </summary>
  public struct Damage
  {
    private readonly int _remaining;

    /// <summary> Constructor. </summary>
    /// <param name="remaining"> The amount of damage done. </param>
    public Damage(int remaining)
    {
      _remaining = remaining;
    }

    /// <summary> The amount of damage done. </summary> 
    public int DamageAmount
    {
      get { return _remaining; }
    }
  }
}