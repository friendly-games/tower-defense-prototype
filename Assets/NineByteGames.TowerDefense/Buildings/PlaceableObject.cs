using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Buildings
{
  /// <summary>
  ///  An object that can be placed in the world and which must be navigated around for path finding.
  /// </summary>
  internal class PlaceableObject : ScriptableObject
  {
    [Tooltip("The non-displayed name of the item")]
    public string Name;

    [Tooltip("The item that can be placed in the world")]
    public GameObject Template;

    [Tooltip("The item that displays when the user is attempting to show the item")]
    public GameObject PreviewItem;

    [Tooltip("The number of grid units the object takes up")]
    public BuildingShape ShapeSize;

    /// <summary> The strategy associated with the given object. </summary>
    public IBuildingShapeStrategy Strategy { get; private set; }

    /// <summary> Invoked when the object is initialized. </summary>
    public void OnEnable()
    {
      Strategy = BuildingShapeStrategies.GetStrategyFor(ShapeSize);
    }
  }
}