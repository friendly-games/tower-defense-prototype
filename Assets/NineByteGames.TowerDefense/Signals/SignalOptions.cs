using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> Options to control the flow of signal. </summary>
  public class SignalOptions : IDisposable
  {
    /// <summary> The options for the current message being sent. </summary>
    public static SignalOptions Current { get; internal set; }

    /// <summary> True if the current signal should continue. </summary>
    public bool ShouldContinue { get; set; }

    /// <summary>
    ///  Resets this object back to a clean state so that it can be used for another message.
    /// </summary>
    internal void Reset()
    {
      ShouldContinue = true;
    }

    void IDisposable.Dispose()
    {
      SignalListenerContext.Pop();
    }

    /// <summary> Sets ShouldContinue to false. </summary>
    public void StopProcessing()
    {
      ShouldContinue = false;
    }
  }
}