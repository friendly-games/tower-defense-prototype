using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Messages
{
  /// <summary> Indicates that a new target has been acquired. </summary>
  public struct TargetAquiredSignal
  {
    private readonly GameObject _target;

    /// <summary> Indicates that the target was lost. </summary>
    public static TargetAquiredSignal TargetLost = new TargetAquiredSignal(null);

    /// <summary> Constructor. </summary>
    /// <param name="target"> The target that has been acquired. </param>
    public TargetAquiredSignal(GameObject target)
    {
      _target = target;
    }

    /// <summary> The target that has been acquired.  </summary>
    public GameObject Target
    {
      get { return _target; }
    }

    /// <summary> True if this is a message indicating that there is no longer a target. </summary>
    public bool TargetWasLost
    {
      get { return Target == null; }
    }
  }
}