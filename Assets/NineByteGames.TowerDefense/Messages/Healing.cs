using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Utils;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace NineByteGames.TowerDefense.Messages
{
  /// <summary> Healing sent to an object. </summary>
  public class Healing
  {
    private int _remaining;

    /// <summary> Constructor. </summary>
    /// <param name="remaining"> The amount of healing sent. </param>
    public Healing(int remaining)
    {
      _remaining = remaining;
    }

    /// <summary> The amount the unit should be healed. </summary>
    public int Remaining
    {
      get { return _remaining; }
    }

    /// <summary>
    ///  Takes a designated amount of healing to take, decrementing <see cref="Remaining"/> the given
    ///  amount with an upper limit of <see cref="Remaining"/> and a lower limit of 0.
    /// </summary>
    /// <param name="amountToTake"> The amount to take. </param>
    public int Take(int amountToTake)
    {
      amountToTake = MathUtils.Clamp(amountToTake, 0, _remaining);

      _remaining -= amountToTake;

      return amountToTake;
    }
  }
}