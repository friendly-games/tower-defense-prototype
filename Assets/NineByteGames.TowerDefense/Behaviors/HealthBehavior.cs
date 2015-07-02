using NineByteGames.TowerDefense.Messages;
using NineByteGames.TowerDefense.Signals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Behaviors
{
  /// <summary> An object that contains health. </summary>
  internal class HealthBehavior : SignalReceiverBehavior<HealthBehavior>, IReadable
  {
    public int Health = 200;
    public int MaxHealth = 500;

    static HealthBehavior()
    {
      SignalEntryPoint.For<HealthBehavior>()
                      .Register(AllSignals.Health, (i, d) => i.Handle(d))
                      .Register(AllSignals.Damage, (i, d) => i.Handle(d))
                      .Register(AllSignals.Death, i => i.HandleDeath());

    }

    void IReadable.AddText(ReadableText builder)
    {
      builder.AddProperty("Health", Health);
      builder.AddProperty("Maximum Health", MaxHealth);
    }

    /// <inheritdoc/>
    public void Handle(Damage damage)
    {
      Health -= damage.DamageAmount;

      if (Health <= 0)
      {
        Broadcaster.Send(AllSignals.Death);
      }

      SignalOptions.Current.StopProcessing();
    }

    /// <inheritdoc/>
    public void HandleDeath()
    {
      DestroyOwner();
    }

    /// <inheritdoc/>
    public void Handle(Healing health)
    {
      Health += health.Take(MaxHealth - Health);
    }
  }
}