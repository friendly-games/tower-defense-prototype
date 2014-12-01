using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> The result of a signal listener. </summary>
  public enum SignalListenerResult
  {
    Continue,
    StopProcessing,
  }

  ///// <summary> Determines how a result was handled. </summary>
  //public struct HandleResult
  //{
  //  public static readonly HandleResult Handled;
  //  public static readonly HandleResult Unhandled;

  //  private const int ConstUnhandled = 0;
  //  private const int ConstHandled = 1;

  //  static HandleResult()
  //  {
  //    Unhandled = new HandleResult(ConstUnhandled);
  //    Handled = new HandleResult(ConstHandled);
  //  }

  //  private HandleResult(int state)
  //  {
  //    _state = state;
  //  }

  //  private readonly int _state;

  //  /// <summary> True if the signal was handled, false otherwise. </summary>
  //  public bool WasHandled
  //  {
  //    get { return _state == ConstUnhandled; }
  //  }
  //}
}