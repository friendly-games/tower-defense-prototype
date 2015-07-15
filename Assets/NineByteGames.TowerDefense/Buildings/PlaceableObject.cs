using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NineByteGames.TowerDefense.General;
using NineByteGames.TowerDefense.Items;
using NineByteGames.TowerDefense.Player;
using NineByteGames.TowerDefense.Services;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.Unity;
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

    [Tooltip("The price of the item")]
    public Money Money;

    #endregion

    /// <summary> The strategy associated with the given object. </summary>
    public IBuildingShapeStrategy Strategy { get; private set; }

    /// <summary> Invoked when the object is initialized. </summary>
    [UnityMethod]
    public void OnEnable()
    {
      Strategy = BuildingShapeStrategies.GetStrategyFor(ShapeSize);
    }

    /// <inheritdoc />
    public IInventoryInstance CreateInstance(IPlayer player)
    {
      // TODO can we cache this somehow TODO set the parent to be the player (currently we don't do
      // this because then the preview turns with the player)
      var previewItem = PreviewItem.Clone();
      previewItem.name = "<Preview Item>";
      var instance = previewItem.AddComponent<PlacableObjectInstanceBehavior>();
      instance.Initialize(this, player.Cursor);
      return instance;
    }

    /// <inheritdoc />
    string IInventoryItemBlueprint.Name
    {
      get { return this.Name; }
    }

    [UsedImplicitly]
    private class PlacableObjectInstanceBehavior : AttachedBehavior, IInventoryInstance
    {
      private IPlayerCursor _cursor;
      private PlaceableObject _owner;

      public void Initialize(PlaceableObject owner, IPlayerCursor cursor)
      {
        _owner = owner;
        _cursor = cursor;
      }

      [UnityMethod]
      public void Update()
      {
        var lowerLeft = GridCoordinate.FromVector3(_cursor.PositionAbsolute);
        this.transform.position = _owner.Strategy.ConvertToGameObjectPosition(lowerLeft);
      }

      /// <inheritdoc />
      public string Name
      {
        get { return _owner.name; }
      }

      /// <inheritdoc />
      public bool Trigger()
      {
        var lowerLeft = GridCoordinate.FromVector3(_cursor.PositionAbsolute);

        if (Managers.Placer.CanCreate(lowerLeft, _owner))
        {
          Managers.Placer.PlaceAt(lowerLeft, _owner);
          return true;
        }

        return false;
      }

      /// <inheritdoc />
      public void MarkDone()
      {
        Owner.Kill();
      }
    }
  }
}