using System;
using System.Linq;
using System.Collections.Generic;
using NineByteGames.TowerDefense.General;

namespace NineByteGames.TowerDefense.Player
{
  /// <summary> Represents the inventory/storage for a player. </summary>
  internal interface IPlayerInventory
  {
    /// <summary> Gets the number of instances of the given item. </summary>
    /// <param name="item"> The item blueprint to count.. </param>
    /// <returns> The count of the item in inventory. </returns>
    int GetCountOf(IInventoryItemBlueprint item);

    /// <summary> Adds a quantity of items to the players inventory. </summary>
    /// <param name="item"> The item for which the quantity should be added. </param>
    /// <param name="count"> The quantity of items to add to inventory. </param>
    /// <returns> True if the quantity of items fit and were added, false otherwise. </returns>
    bool Add(IInventoryItemBlueprint item, int count);

    /// <summary> Subtracts the number of items from the player's inventory. </summary>
    /// <param name="item"> The item for which the quantity should be removed. </param>
    /// <param name="count"> The quantity of items to be removed from inventory. </param>
    /// <returns> True if the items were removed, false if they were not. </returns>
    bool Remove(IInventoryItemBlueprint item, int count);
  }
}