using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Behaviors.Tracking;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.Unity;
using UnityEngine;

namespace NineByteGames.TowerDefense.AI
{
  /// <summary>
  ///  A child that is part of a larger group that acts together towards a common goal.
  /// </summary>
  public class GroupChildBehavior : AttachedBehavior
  {
    private IGroupGuider _parent;

    public void Initialize(IGroupGuider parent)
    {
      _parent = parent;
      _parent.Attach(this.gameObject);
    }

    [UnityMethod]
    public void OnDestroy()
    {
      _parent.Remove(this.gameObject);
    }
  }
}