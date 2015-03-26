using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.Common.Extensions
{
  /// <summary> Extension methods to arrays. </summary>
  internal static class ArrayExtensions
  {
    /// <summary> Check if the given index is valid for the given array. </summary>
    /// <param name="array"> The array to act on. </param>
    /// <param name="index"> The index to check. </param>
    /// <returns> true if index is non-negative and less than the length of the array, false if not. </returns>
    public static bool IsIndexValid<T>(this T[] array, int index)
    {
      return index >= 0 && index < array.Length;
    }
  }
}