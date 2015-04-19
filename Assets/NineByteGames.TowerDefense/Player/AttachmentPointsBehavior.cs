using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;

namespace NineByteGames.TowerDefense.Player
{
  /// <summary>
  ///  Allows retrieving the AttachmentToPositionLookup associated with the object.
  /// </summary>
  /// <remarks>
  ///  For the placement of objects, rather than have the position/rotation defined abstractly or in
  ///  code, we allow the designer to place fake objects that serve as placeholder for the objects
  ///  that will ultimately be placed there.  This object collects all of those placeholder objects,
  ///  removes them, but then stores their original position/rotation so that they can be returned
  ///  in the lookup property.
  /// </remarks>
  public class AttachmentPointsBehavior : AttachedBehavior
  {
    private AttachmentToPositionLookup _lookup;

    /// <summary> Create the AttachmentToPositionLookup. </summary>
    public void Start()
    {
      var lookup = new Dictionary<int, LocationAndRotation>();
      var children = GetComponentsInChildren<AttachmentTypeBehavior>();

      foreach (var child in children)
      {
        lookup.Add((int)child.Type,
                   new LocationAndRotation(child.transform.localPosition, child.transform.localRotation));
        child.gameObject.Kill();
      }

      _lookup = new AttachmentToPositionLookup(lookup);
    }

    /// <summary>
    ///  Retrieves the AttachmentToPositionLookup associated with an object and then removes the
    ///  GameObject/component that the lookup came from.
    /// </summary>
    public static AttachmentToPositionLookup RetrieveFor(GameObject gameObject)
    {
      var instance = gameObject.GetComponentInChildren<AttachmentPointsBehavior>();
      var lookup = instance._lookup;

      if (instance.gameObject != gameObject)
      {
        instance.gameObject.Kill();
      }
      else
      {
        GameObject.Destroy(instance);
      }

      return lookup;
    }
  }
}