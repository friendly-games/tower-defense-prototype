using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.AI;
using NineByteGames.TowerDefense.Behaviors;
using NineByteGames.TowerDefense.Behaviors.World;
using NineByteGames.TowerDefense.World;
using NineByteGames.TowerDefense.World.Grid;
using UnityEngine;

namespace NineByteGames.TowerDefense.Objects
{
  /// <summary> Aids in the placement of objects in the world. </summary>
  internal class ObjectWorldPlacement : IInstanceManager
  {
    private readonly WorldGrid _worldGrid;

    /// <summary> Constructor. </summary>
    /// <param name="worldGrid"> The world grid. </param>
    public ObjectWorldPlacement(WorldGrid worldGrid)
    {
      _worldGrid = worldGrid;
    }

    /// <summary> True if the given object can be placed at the given location. </summary>
    /// <param name="lowerLeft"> The lower left position of the place where the object may be placed. </param>
    /// <param name="placeable"> The object that may be placed at the given location. </param>
    /// <returns> True if the object can be placed at the location, false otherwise. </returns>
    public bool CanCreate(GridCoordinate lowerLeft, PlaceableObject placeable)
    {
      return _worldGrid.IsEmpty(lowerLeft, placeable.Strategy.Size);
    }

    /// <summary> Place an instance of <paramref name="placeable"/> at the given coordinate. </summary>
    /// <param name="lowerLeft"> The lower left position of the place where the object should be
    ///  placed. </param>
    /// <param name="placeable"> The object that should be placed at the given location. </param>
    /// <returns> A clone of the template of <paramref name="placeable"/>. </returns>
    public GameObject PlaceAt(GridCoordinate lowerLeft, PlaceableObject placeable)
    {
      var position = placeable.Strategy.ConvertToGameObjectPosition(lowerLeft);
      var clone = Create(placeable, position, Quaternion.identity);

      return clone;
    }

    /// <summary> Creates a new instance of the given prefab. </summary>
    /// <param name="prefab"> The prefab to create an instance of. </param>
    /// <param name="position"> The location at which the new instance should be placed. </param>
    /// <param name="euler"> The rotation at which the new instance should be placed. </param>
    /// <returns> A GameObject representing the newly created instance. </returns>
    private GameObject Create(PlaceableObject prefab, Vector3 position, Quaternion euler)
    {
      var cloned = prefab.Template.Clone(position, euler);

      var prefabBased = cloned.AddComponent<LifetimeManagementBehavior>();
      prefabBased.Manager = this;
      prefabBased.Tag = prefab;

      return cloned;
    }

    public void NotifyAlive(GameObject instance)
    {
      var placeable = instance.GetComponent<LifetimeManagementBehavior>()
                              .GetTag<PlaceableObject>();

      var strategy = placeable.Strategy;
      var position = instance.GetComponent<Transform>().position;
      var lowerLeft = strategy.ConvertFromGameObjectPosition(position);

      _worldGrid.Set(lowerLeft, placeable.Strategy.Size, new CellData { Type = CellType.Wall});
    }

    public void NotifyDestroy(GameObject instance)
    {
      var placeable = instance.GetComponent<LifetimeManagementBehavior>()
                              .GetTag<PlaceableObject>();

      var strategy = placeable.Strategy;
      var position = instance.GetComponent<Transform>().position;
      var lowerLeft = strategy.ConvertFromGameObjectPosition(position);

      _worldGrid.Set(lowerLeft, placeable.Strategy.Size, new CellData { RawType = 0 });
    }
  }
}