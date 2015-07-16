using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Messages;
using NineByteGames.TowerDefense.Signals;

namespace NineByteGames.TowerDefense.Behaviors
{
  /// <summary> Handles money being collected. </summary>
  internal class MoneyCollector : SignalReceiverBehavior<MoneyCollector>, IReadable
  {
    private int _amount;

    static MoneyCollector()
    {
      SignalEntryPoint.For<MoneyCollector>()
                      .Register(AllSignals.MoneyTransfer, (i, d) => i.Handle(d), AllPriorities.VeryHigh);
    }

    private void Handle(MoneyTransfer message)
    {
      _amount += message.Amount;
    }

    public void AddText(ReadableText builder)
    {
      builder.AddProperty("Collected Money", _amount);
    }
  }
}