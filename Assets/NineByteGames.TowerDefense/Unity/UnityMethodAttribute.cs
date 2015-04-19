using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Unity
{
  /// <summary> Method to indicate that it is a unity event. </summary>
  [AttributeUsage(AttributeTargets.Method, Inherited = false)]
  internal sealed class UnityMethodAttribute : Attribute
  {
    /// <summary> Default constructor. </summary>
    public UnityMethodAttribute()
    {
    }

    /// <summary> Constructor. </summary>
    /// <param name="comment"> A comment describing the use of the method. </param>
    public UnityMethodAttribute(string comment)
    {
    }
  }
}