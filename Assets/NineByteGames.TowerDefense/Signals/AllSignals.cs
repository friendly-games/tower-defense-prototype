using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Behaviors;
using NineByteGames.TowerDefense.Items;
using NineByteGames.TowerDefense.Messages;

namespace NineByteGames.TowerDefense.Signals
{
  internal class AllSignals
  {
    public static readonly SignalType<Damage> Damage = new SignalType<Damage>();
    public static readonly SignalType<Healing> Health = new SignalType<Healing>();
    public static readonly SignalType<TargetAquiredSignal> TargetChanged = new SignalType<TargetAquiredSignal>();
    public static readonly SignalType<MoneyTransfer> MoneyTransfer = new SignalType<MoneyTransfer>();


    /// <summary> When two payloads have merged. </summary>
    public static readonly SignalType<PayloadBehavior> Merged = new SignalType<PayloadBehavior>();

    public static readonly SignalType Death = new SignalType();
  }
}