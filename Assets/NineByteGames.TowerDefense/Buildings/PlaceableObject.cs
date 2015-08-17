using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NineByteGames.TowerDefense.Behaviors.World;
using NineByteGames.TowerDefense.General;
using NineByteGames.TowerDefense.Items;
using NineByteGames.TowerDefense.Player;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.Unity;
using NineByteGames.TowerDefense.World;
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
    public Money Cost;

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
    public IInventoryInstance CreateInstance(IWorld world, IPlayer player)
    {
      // TODO can we cache this somehow TODO set the parent to be the player (currently we don't do
      // this because then the preview turns with the player)
      var previewItem = PreviewItem.Clone();
      previewItem.name = "<Preview Item>";
      var instance = previewItem.AddComponent<PlacableObjectInstanceBehavior>();
      instance.Initialize(this, player.Cursor, player.Bank, world.Placer);

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
      private IMoneyBank _bank;
      private BuildingWorldPlacement _placer;

      public void Initialize(PlaceableObject owner, IPlayerCursor cursor, IMoneyBank bank, BuildingWorldPlacement placer)
      {
        _bank = bank;
        _owner = owner;
        _cursor = cursor;
        _placer = placer;
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
        // TODO we should let the player know somehow
        if (!_bank.CanAfford(_owner.Cost))
          return false;

        var lowerLeft = GridCoordinate.FromVector3(_cursor.PositionAbsolute);

        if (!_placer.CanCreate(lowerLeft, _owner))
          return false;

        _placer.PlaceAt(lowerLeft, _owner);
        _bank.Deduct(_owner.Cost);

        return true;
      }

      /// <inheritdoc />
      public void MarkDone()
      {
        Owner.Kill();
      }
    }
  }
}