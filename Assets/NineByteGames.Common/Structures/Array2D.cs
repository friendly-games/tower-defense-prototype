using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.Common.Structures
{
  /// <summary> A 2d array based by a normal array. </summary>
  /// <typeparam name="T"> Generic type parameter. </typeparam>
  internal class Array2D<T>
  {
    /// <summary> Constructor. </summary>
    /// <param name="width"> The width of the array. </param>
    /// <param name="height"> The height of the array. </param>
    public Array2D(int width, int height)
    {
      Width = width;
      Height = height;

      RawData = new T[width * height];
    }

    /// <summary> The width of the array. </summary>
    public int Width { get; private set; }

    /// <summary> The height of the array. </summary>
    public int Height { get; private set; }

    /// <summary> The array underneath the array. </summary>
    public T[] RawData { get; private set; }

    /// <summary>
    ///  Get the element at the specified index.
    /// </summary>
    /// <param name="x"> The x coordinate. </param>
    /// <param name="y"> The y coordinate. </param>
    public T this[int x, int y]
    {
      get { return RawData[Index(x, y)]; }
      set { RawData[Index(x, y)] = value; }
    }

    /// <summary> Indexes. </summary>
    /// <param name="x"> The x coordinate. </param>
    /// <param name="y"> The y coordinate. </param>
    /// <returns> An int. </returns>
    private int Index(int x, int y)
    {
      return y * Width + x;
    }

    /// <summary> Gets an iterator pointing at the beginning of the array. </summary>
    /// <returns> The iterator. </returns>
    public Iterator GetIterator()
    {
      return new Iterator(this, 0);
    }

    /// <summary> Allows efficient iteration of an Array2d in row-column order </summary>
    public struct Iterator
    {
      private readonly Array2D<T> _array;
      private int _index;

      /// <summary> Constructor. </summary>
      /// <param name="array"> The array to iterate. </param>
      /// <param name="startIndex"> The start index. </param>
      internal Iterator(Array2D<T> array, int startIndex)
      {
        _array = array;
        _index = startIndex;
      }

      /// <summary> Move the index forward. </summary>
      public void MoveNext()
      {
        _index++;
      }

      /// <summary> Gets or sets the value at the current index. </summary>
      public T Value
      {
        get { return _array.RawData[_index]; }
        set { _array.RawData[_index] = value; }
      }
    }
  }
}