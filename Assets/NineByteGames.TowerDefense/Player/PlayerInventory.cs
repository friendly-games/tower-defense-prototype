using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.General;

namespace NineByteGames.TowerDefense.Player
{
  /// <summary> Concreate implementation of the player's inventory. </summary>
  internal class PlayerInventory : IPlayerInventory
  {
    private readonly Dictionary<IInventoryItemBlueprint, int> _storage
      = new Dictionary<IInventoryItemBlueprint, int>();

    /// <inheritdoc/>
    int IPlayerInventory.GetCountOf(IInventoryItemBlueprint item)
    {
      int count;
      if (_storage.TryGetValue(item, out count))
        return count;

      return 0;
    }

    /// <inheritdoc/>
    bool IPlayerInventory.Add(IInventoryItemBlueprint item, int count)
    {
      if (count <= 0)
        throw new ArgumentException("Count must be > 0 ", "count");

      int exitingCount;

      if (_storage.TryGetValue(item, out exitingCount))
      {
        count += exitingCount;
      }

      _storage[item] = count;

      return true;
    }

    /// <inheritdoc/>
    bool IPlayerInventory.Remove(IInventoryItemBlueprint item, int count)
    {
      if (count <= 0)
        throw new ArgumentException("Count must be > 0 ", "count");

      int existingCount;

      // make sure we have enough items to actually remove
      if (!_storage.TryGetValue(item, out existingCount) || existingCount < count)
        return false;

      _storage[item] = existingCount - count;

      return true;
    }
  }
}