using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.Common.Extensions;

namespace NineByteGames.TowerDefense.Utils
{
  /// <summary> Maintains an array of data and an associated current item. </summary>
  internal class DataCollection<T>
  {
    /// <summary> The data that provides the current value for the "selected" item. </summary>
    private readonly T[] _data;

    private int _selectedIndex;

    /// <summary> Constructor. </summary>
    public DataCollection(T[] data)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      if (data.Length == 0)
        throw new ArgumentException("Data array must have at least one element", "data");

      _data = data;
      _selectedIndex = 0;
    }

    /// <summary> The currently selected item. </summary>
    public T Selected
    {
      get { return _data[_selectedIndex]; }
    }

    /// <summary> The number of items in the collection. </summary>
    public int Count
    {
      get { return _data.Length; }
    }

    /// <summary> Attempt to set the specified index as the current item in the array. </summary>
    /// <param name="index"> The index of the item to set. </param>
    /// <returns> True if the index was set, false if it was invalid or the same value as before. </returns>
    public bool SetSelectedIndex(int index)
    {
      if (index == _selectedIndex || !_data.IsIndexValid(index))
        return false;

      _selectedIndex = index;
      return true;
    }
  }
}