using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Messages
{
  /// <summary> Represents the amount of damage something inflicts. </summary>
  public struct Damage
  {
    private readonly float _damageAmount;

    /// <summary> Constructor. </summary>
    /// <param name="damageAmount"> The amount of damage done. </param>
    public Damage(float damageAmount)
    {
      _damageAmount = damageAmount;
    }

    /// <summary> The amount of damage done. </summary> 
    public float DamageAmount
    {
      get { return _damageAmount; }
    }
  }
}