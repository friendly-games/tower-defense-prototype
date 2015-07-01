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
                                  ISignalListener<DeathIndicator>
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
    public void Handle(Damage damage)
    {
      Health -= damage.DamageAmount;

      if (Health <= 0)
      {
        Broadcaster.Send(DeathIndicator.Signal);
      }

      SignalOptions.Current.StopProcessing();
    }

    /// <inheritdoc/>
    [SignalPriority(SignalPriorities.VeryLow)]
    public void Handle(DeathIndicator health)
    {
      DestroyOwner();
    }

    /// <inheritdoc/>
    [SignalPriority(SignalPriorities.VeryLow)]
    public void Handle(Healing health)
    {
      float amountToHeal = Math.Min(health.Remaining, MaxHealth - Health);
      health.Take(amountToHeal);

      Health += amountToHeal;
    }
  }
}