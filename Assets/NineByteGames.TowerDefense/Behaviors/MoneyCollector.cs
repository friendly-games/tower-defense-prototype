using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Messages;
using NineByteGames.TowerDefense.Signals;

namespace NineByteGames.TowerDefense.Behaviors
{
  /// <summary> Handles money being collected. </summary>
  internal class MoneyCollector : ChildBehavior, IReadable, ISignalListener<MoneyTransfer>
  {
    private int _amount;

    public void Handle(MoneyTransfer message)
    {
      _amount += message.Amount;

      SignalOptions.Current.StopProcessing();
    }

    public void AddText(ReadableText builder)
    {
      builder.AddProperty("Money", _amount);
    }
  }
}