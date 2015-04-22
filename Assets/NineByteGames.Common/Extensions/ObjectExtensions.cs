using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.Common.Extensions
{
  /// <summary> Extension methods to Object. </summary>
  public static class ObjectExtensions
  {
    /// <summary> Cast the given object to type T. </summary>
    /// <typeparam name="T"> Generic type parameter. </typeparam>
    /// <param name="instance"> The instance to cast. </param>
    /// <returns> <paramref name="instance"/> casted to type T. </returns>
    public static T CastTo<T>(this object instance)
      where T : class
    {
      return (T)instance;
    }
  }
}