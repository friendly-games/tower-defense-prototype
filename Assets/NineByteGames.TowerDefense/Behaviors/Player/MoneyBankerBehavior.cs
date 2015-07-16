using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Items;
using NineByteGames.TowerDefense.Messages;
using NineByteGames.TowerDefense.Player;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.Unity;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors.Player
{
  /// <summary> Retrieves money collected by the user and adds it to the bank. </summary>
  public class MoneyBankerBehavior : SignalReceiverBehavior<MoneyBankerBehavior>, IReadable
  {
    private IPlayer _player;

    static MoneyBankerBehavior()
    {
      SignalEntryPoint.For<MoneyBankerBehavior>()
                      .Register(AllSignals.MoneyTransfer, (behavior, transfer) => behavior.HandleMoney(transfer), AllPriorities.VeryLow);
    }

    [UnityMethod]
    public override void Start()
    {
      base.Start();
      _player = this.GetComponent<IPlayer>();
    }

    private void HandleMoney(MoneyTransfer transfer)
    {
      Debug.Log("Here");
      _player.Bank.Add(new Money(transfer.Amount));
    }

    /// <inheritdoc />
    void IReadable.AddText(ReadableText builder)
    {
      builder.AddProperty("Bank Money", _player.Bank.CurrentAmount);
    }
  }
}