using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Messages;

namespace NineByteGames.TowerDefense.Signals
{
  public class AllSignals
  {
    public static readonly SignalType<Damage> Damage = new SignalType<Damage>();
    public static readonly SignalType<Healing> Health = new SignalType<Healing>();
    public static readonly SignalType<TargetAquiredSignal> TargetChanged = new SignalType<TargetAquiredSignal>();
    public static readonly SignalType<MoneyTransfer> MoneyTransfer = new SignalType<MoneyTransfer>();

    public static readonly SignalType Death = new SignalType();
  }
}