using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> Indicates the priority of a given ISignalListener. </summary>
  [AttributeUsage(AttributeTargets.Method, Inherited = false)]
  internal sealed class SignalPriorityAttribute : Attribute
  {
    /// <summary> Constructor. </summary>
    /// <param name="priority"> The priority of a given handler. </param>
    public SignalPriorityAttribute(int priority)
    {
      Priority = priority;
    }

    /// <summary> The priority of a given handler. </summary>
    public int Priority { get; private set; }
  }
}