using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Messages
{
  /// <summary> Healing sent to an object. </summary>
  public class Healing
  {
    private float _remaining;

    /// <summary> Constructor. </summary>
    /// <param name="remaining"> The amount of healing sent. </param>
    public Healing(float remaining)
    {
      _remaining = remaining;
    }

    /// <summary> The amount the unit should be healed. </summary>
    public float Remaining
    {
      get { return _remaining; }
    }

    /// <summary> Takes a designated amount of healing to take. </summary>
    /// <param name="amount"> The amount to take. </param>
    public void Take(float amount)
    {
      if (amount > _remaining)
        throw new Exception("Too much taken");

      _remaining = Math.Max(0, _remaining - amount);
    }
  }
}