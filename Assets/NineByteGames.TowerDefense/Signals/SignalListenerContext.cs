using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary>
  ///  Provides methods for managing the <see cref="SignalOptions.Current"/>.
  /// </summary>
  public static class SignalListenerContext
  {
    private static readonly Stack<SignalOptions> Stack;
    private static readonly Stack<SignalOptions> Recycled;

    static SignalListenerContext()
    {
      Stack = new Stack<SignalOptions>();
      Recycled = new Stack<SignalOptions>();

      SignalOptions.Current = new SignalOptions();
    }

    /// <summary>
    ///  Pushes a new options to be the Current, saving the current options until Pop is called again.
    /// </summary>
    internal static SignalOptions Push()
    {
      Debug.Assert(SignalOptions.Current != null);

      Stack.Push(SignalOptions.Current);

      var next = Recycled.Count > 0
        ? Recycled.Pop()
        : new SignalOptions();

      next.Reset();

      return SignalOptions.Current;
    }

    /// <summary> Removes the last options and restores the last options object. </summary>
    internal static void Pop()
    {
      Recycled.Push(SignalOptions.Current);

      SignalOptions.Current = Stack.Pop();

      Debug.Assert(SignalOptions.Current != null);
    }
  }
}