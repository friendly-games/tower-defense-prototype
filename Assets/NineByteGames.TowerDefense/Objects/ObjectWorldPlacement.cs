using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Behaviors.World;
using NineByteGames.TowerDefense.World;
using NineByteGames.TowerDefense.World.Grid;
using UnityEditor;
using UnityEngine;
using MathUtils = NineByteGames.TowerDefense.Utils.MathUtils;

namespace NineByteGames.TowerDefense.Objects
{
  internal class ObjectWorldPlacement
  {
    private readonly WorldGrid _worldGrid;

    public ObjectWorldPlacement(WorldGrid worldGrid)
    {
      _worldGrid = worldGrid;
    }

    public bool CanCreate(GridCoordinate lowerLeft, PlaceableObject placeable)
    {
      return true;
    }

    public GameObject PlaceAt(GridCoordinate lowerLeft, PlaceableObject placeable)
    {
      var position = placeable.ConvertToGameObjectPosition(lowerLeft);
      var clone = Create(placeable.Template, position, Quaternion.identity);

      GridUpdate.MarkWalkable(lowerLeft, placeable.Size, false);
      return clone;
    }

    /// <summary> Creates a new instance of the given prefab. </summary>
    /// <param name="prefab"> The prefab to create an instance of. </param>
    /// <param name="position"> The location at which the new instance should be placed. </param>
    /// <param name="euler"> The rotation at which the new instance should be placed. </param>
    /// <returns> A GameObject representing the newly created instance. </returns>
    protected GameObject Create(GameObject prefab, Vector3 position, Quaternion euler)
    {
      var cloned = prefab.Clone(position, euler);
      //cloned.SetParent(Parent);

      //var prefabBased = cloned.AddComponent<PrefabBasedObjectBehavior>();
      //prefabBased.Manager = this;

      return cloned;
    }
  }
}