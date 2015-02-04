using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.Common.Extensions
{
  /// <summary> Extension method to delegates. </summary>
  public static class DelegateExtensions
  {
    /// <summary>
    ///  Invoke an action if it is non-null.
    /// </summary>
    /// <param name="action"> The action to act on. </param>
    /// <param name="value1"> The first value to pass to the action to execute. </param>
    /// <param name="value2"> The second value to pass to the action to execute. </param>
    public static void InvokeSafe<T1, T2>(this Action<T1, T2> action, T1 value1, T2 value2)
    {
      if (action != null)
      {
        action(value1, value2);
      }
    }
  }
}