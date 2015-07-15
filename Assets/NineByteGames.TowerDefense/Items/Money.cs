using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using UnityEngine;

namespace NineByteGames.TowerDefense.Items
{
  /// <summary> Represents money in the game. </summary>
  [Serializable]
  public struct Money
  {
    [Tooltip("The cost of the item")]
    [SerializeField]
    private int _amount;

    /// <summary> Zero amount of money. </summary>
    public static Money None = new Money(0);

    /// <summary> Constructor. </summary>
    /// <param name="amount"> The amount of money that is present. </param>
    public Money(int amount)
    {
      Debug.Assert(amount >= 0);
      _amount = Math.Abs(amount);
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return _amount.ToString();
    }

    #region Operators

    public static bool operator ==(Money left, Money right)
    {
      return left._amount == right._amount;
    }

    public static bool operator !=(Money left, Money right)
    {
      return left._amount != right._amount;
    }

    public static bool operator <=(Money left, Money right)
    {
      return left._amount <= right._amount;
    }

    public static bool operator >=(Money left, Money right)
    {
      return left._amount >= right._amount;
    }

    public static bool operator <(Money left, Money right)
    {
      return left._amount < right._amount;
    }

    public static bool operator >(Money left, Money right)
    {
      return left._amount > right._amount;
    }

    public static Money operator +(Money left, Money right)
    {
      return new Money(left._amount + right._amount);
    }

    public static Money operator -(Money left, Money right)
    {
      return new Money(left._amount - right._amount);
    }

    #endregion
  }
}