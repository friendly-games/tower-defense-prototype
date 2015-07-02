using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Messages
{
  /// <summary> Transfers money from one thing to anther. </summary>
  public struct MoneyTransfer
  {
    private readonly int _amount;

    /// <summary> Constructor. </summary>
    /// <param name="amount"> The amount of money that is being transferred. </param>
    public MoneyTransfer(int amount)
    {
      _amount = amount;
    }

    public int Amount
    {
      get { return _amount; }
    }
  
  }
}