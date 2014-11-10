using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Messages
{
  /// <summary> Represents the amount of damage something inflicts. </summary>
  public struct Damage
  {
    private readonly float _damageAmount;

    public Damage(float damageAmount)
    {
      _damageAmount = damageAmount;
    }

    public float DamageAmount
    {
      get { return _damageAmount; }
    }
  }
}