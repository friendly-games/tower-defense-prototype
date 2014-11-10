using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NineByteGames.TowerDefense.Messages;
using NineByteGames.TowerDefense.Signals;

namespace NineByteGames.TowerDefense.Behaviors
{
  /// <summary> An object that contains health. </summary>
  internal class HealthBehavior : ChildBehavior,
                                  IReadable,
                                  ISignalListener<Damage>,
                                  ISignalListener<Healing>,
                                  ISignalListener<SignalIndicators.DeathIndicator>
  {
    public float Health = 200;
    public float MaxHealth = 500;

    /// <inheritdoc/>
    [SignalPriority(SignalPriorities.VeryLow)]
    public bool Handle(Damage damage)
    {
      Health -= damage.DamageAmount;

      if (Health <= 0)
      {
        RootBehavior.Broadcaster.Send(SignalIndicators.Death);
      }

      return true;
    }

    /// <inheritdoc/>
    [SignalPriority(SignalPriorities.VeryLow)]
    public bool Handle(SignalIndicators.DeathIndicator health)
    {
      DestroyOwner();
      return true;
    }

    /// <inheritdoc/>
    [SignalPriority(SignalPriorities.VeryLow)]
    public bool Handle(Healing health)
    {
      float amountToHeal = Math.Min(health.Remaining, MaxHealth - Health);
      health.Take(amountToHeal);

      Health += amountToHeal;

      return true;
    }

    void IReadable.AddText(ReadableText builder)
    {
      builder.AddProperty("Health", Health);
      builder.AddProperty("Maximum Health", MaxHealth);
    }
  }
}