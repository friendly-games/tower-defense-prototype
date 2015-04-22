using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Player;
using UnityEngine;

namespace NineByteGames.TowerDefense.General
{
  /// <summary> An item that can be stored in inventory and be triggered. </summary>
  internal interface ITriggerableItem
  {
    /// <summary> The displayed name </summary>
    string Name { get; }

    /// <summary> Trigger the given item. </summary>
    /// <returns> true if it succeeds, false if it fails. </returns>
    bool Trigger();

    /// <summary> Cursor Item. </summary>
    GameObject PreviewItem { get; }

    /// <summary> Mark the object as no longer used by the player. </summary>
    void MarkDone();
  }

  /// <summary> An blueprint for an item that can be stored in inventory. </summary>
  internal interface IInventoryItemBlueprint
  {
    /// <summary> Creates the instance. </summary>
    /// <returns> The player specific instance of the inventory item. </returns>
    ITriggerableItem CreateInstance(IPlayer player);
  }
}