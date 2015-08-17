using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Player;
using NineByteGames.TowerDefense.World;
using UnityEngine;

namespace NineByteGames.TowerDefense.General
{
  /// <summary> An item that can be stored in inventory and be triggered. </summary>
  /// <remarks>
  ///  IInventoryInstances should not have any state in them and should not be player specific.
  ///  <see cref="IInventoryItemBlueprint"/> on the other hand can and most likely will
  ///  be player specific.
  /// </remarks>
  /// <seealso cref="IInventoryItemBlueprint"/>
  internal interface IInventoryInstance
  {
    /// <summary> The displayed name </summary>
    string Name { get; }

    /// <summary> Trigger the given item. </summary>
    /// <returns> true if it succeeds, false if it fails. </returns>
    bool Trigger();

    /// <summary> Mark the object as no longer used by the player. </summary>
    void MarkDone();
  }

  /// <summary> An blueprint for an item that can be stored in inventory. </summary>
  /// <seealso cref="IInventoryInstance"/>
  internal interface IInventoryItemBlueprint
  {
    /// <summary> Creates the instance. </summary>
    /// <param name="world"> The world in which the instance should be created. </param>
    /// <param name="player"> The player. </param>
    /// <returns> The player specific instance of the inventory item. </returns>
    IInventoryInstance CreateInstance(IWorld world, IPlayer player);

    /// <summary> The name of the item </summary>
    string Name { get; }
  }
}