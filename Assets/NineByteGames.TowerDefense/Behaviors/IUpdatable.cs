using System;
using System.Collections.Generic;
using System.Linq;

/// <summary> An object that should be updated every frame. </summary>
public interface IUpdatable
{
  /// <summary> Updates the object. </summary>
  void Update();
}