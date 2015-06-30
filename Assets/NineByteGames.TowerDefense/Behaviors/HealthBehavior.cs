using NineByteGames.TowerDefense.Messages;
using NineByteGames.TowerDefense.Signals;
using System;
using System.Collections.Generic;
using System.Linq;

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

    void IReadable.AddText(ReadableText builder)
    {
      builder.AddProperty("Health", Health);
      builder.AddProperty("Maximum Health", MaxHealth);
    }

    /// <inheritdoc/>
    [SignalPriority(SignalPriorities.VeryLow)]
    public SignalListenerResult Handle(Damage damage)
    {
      Health -= damage.DamageAmount;

      if (Health <= 0)
      {
        Broadcaster.Send(SignalIndicators.Death);
      }

      return SignalListenerResult.StopProcessing;
    }

    /// <inheritdoc/>
    [SignalPriority(SignalPriorities.VeryLow)]
    public SignalListenerResult Handle(SignalIndicators.DeathIndicator health)
    {
      DestroyOwner();
      return SignalListenerResult.Continue;
    }

    /// <inheritdoc/>
    [SignalPriority(SignalPriorities.VeryLow)]
    public SignalListenerResult Handle(Healing health)
    {
      float amountToHeal = Math.Min(health.Remaining, MaxHealth - Health);
      health.Take(amountToHeal);

      Health += amountToHeal;

      return SignalListenerResult.Continue;
    }
  }
}