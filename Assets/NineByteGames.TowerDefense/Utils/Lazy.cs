using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Utils
{
  /// <summary> A value that is created on demand. </summary>
  internal class Lazy<T>
    where T : class, new()
  {
    /// <summary> The value that is constructed on-demand. </summary>
    private T _value;

    /// <summary> The value of the on-demand created instance </summary>
    public T Value
    {
      get { return _value ?? (_value = new T()); }
    }
  }
}