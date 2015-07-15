using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Items;

namespace NineByteGames.TowerDefense.Player
{
  /// <summary> Interface for an object that contains money. </summary>
  internal interface IMoneyBank
  {
    /// <summary> True if the bank can afford the given price. </summary>
    /// <param name="money"> The price of the item in question. </param>
    /// <returns> True if the bank can afford it, false otherwise. </returns>
    bool CanAfford(Money money);

    /// <summary> Deducts the given amount from the bank. </summary>
    /// <param name="money"> The price of the item in question. </param>
    void Deduct(Money money);

    /// <summary> Adds the given amount to the bank. </summary>
    /// <param name="money"> The price of the item in question. </param>
    void Add(Money money);

    /// <summary> The amount of money currently in the bank. </summary>
    Money CurrentAmount { get; }
  }
}