using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> Associates a type with a numeric id. </summary>
  /// <typeparam name="T"> Generic type parameter. </typeparam>
  public static class GenericId<T>
  {
    /// <summary> The numeric id associated with the type. </summary>
    public static int Id { get; set; }
  }
}