using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> A behavior that acts as a child of a RootBehavior.</summary>
  public abstract class ChildBehavior : AttachedBehavior
  {
    /// <summary>
    ///  Finds the ultimate parent of this object that contains the RootBehavior for this hiearachy.
    /// </summary>
    /// <returns> The found root parent. </returns>
    public GameObject FindRootParent()
    {
      return FindRootBehavior().gameObject;
    }

    /// <summary> Finds the parent of this object that contains the RootBeavior for this hiearachy. </summary>
    protected RootBehavior FindRootBehavior()
    {
      var rootBehavior = gameObject.RetrieveInHierarchy<RootBehavior>();

      if (rootBehavior == null)
        throw new Exception("No RootBehavior found");
      if (rootBehavior.Broadcaster == null)
        throw new Exception("RootBehavior.Broadcaster is null");

      return rootBehavior;
    }
  }
}