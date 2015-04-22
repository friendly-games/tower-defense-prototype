using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.General;
using NineByteGames.TowerDefense.Player;
using NineByteGames.TowerDefense.Services;
using NineByteGames.TowerDefense.World.Grid;
using UnityEngine;

namespace NineByteGames.TowerDefense.Buildings
{
  /// <summary>
  ///  An object that can be placed in the world and which must be navigated around for path finding.
  /// </summary>
  internal class PlaceableObject : ScriptableObject, IInventoryItemBlueprint
  {
    #region Unity Properties

    [Tooltip("The non-displayed name of the item")]
    public string Name;

    [Tooltip("The item that can be placed in the world")]
    public GameObject Template;

    [Tooltip("The item that displays when the user is attempting to show the item")]
    public GameObject PreviewItem;

    [Tooltip("The number of grid units the object takes up")]
    public BuildingShape ShapeSize;

    #endregion

    /// <summary> The strategy associated with the given object. </summary>
    public IBuildingShapeStrategy Strategy { get; private set; }

    /// <summary> Invoked when the object is initialized. </summary>
    public void OnEnable()
    {
      Strategy = BuildingShapeStrategies.GetStrategyFor(ShapeSize);
    }

    /// <inheritdoc />
    public ITriggerableItem CreateInstance(IPlayer player)
    {
      return new PlaceableObjectItem(player, this);
    }

    private class PlaceableObjectItem : ITriggerableItem
    {
      private readonly PlaceableObject _owner;
      private readonly IPlayer _player;

      public PlaceableObjectItem(IPlayer player, PlaceableObject owner)
      {
        // TODO take into account the player inventory (e.g. resources etc)
        _owner = owner;
        _player = player;
      }

      public string Name
      {
        get { return _owner.Name; }
      }

      public bool Trigger()
      {
        var lowerLeft = GridCoordinate.FromVector3(_player.Cursor.PositionAbsolute);

        if (Managers.Placer.CanCreate(lowerLeft, _owner))
        {
          Managers.Placer.PlaceAt(lowerLeft, _owner);
          return true;
        }

        return false;
      }

      public GameObject PreviewItem
      {
        get { return _owner.PreviewItem; }
      }

      public void MarkDone()
      {
        // no-op
      }
    }
  }
}