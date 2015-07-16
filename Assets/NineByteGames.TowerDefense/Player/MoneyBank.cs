using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NineByteGames.Common.Data;
using NineByteGames.TowerDefense.Items;

namespace NineByteGames.TowerDefense.Player
{
  /// <summary> Concrete implementation of IMoneyBank. </summary>
  internal class MoneyBank : IMoneyBank
  {
    /// <summary> The amount of money currently in the bank. </summary>
    public Money CurrentAmount { get; private set; }

    /// <inheritdoc />
    bool IMoneyBank.CanAfford(Money money)
    {
      return money <= CurrentAmount;
    }

    /// <inheritdoc />
    void IMoneyBank.Deduct(Money money)
    {
      if (money > CurrentAmount)
      {
        CurrentAmount = Money.None;
        Debug.Fail("Amount deducted was more than the amount allowed");
      }
      else
      {
        CurrentAmount -= money;
      }

      AmountChanged.Notify();
    }

    /// <inheritdoc />
    void IMoneyBank.Add(Money money)
    {
      CurrentAmount += money;
      AmountChanged.Notify();
    }

    /// <inheritdoc />
    public Notifee AmountChanged { get; set; }
  }
}