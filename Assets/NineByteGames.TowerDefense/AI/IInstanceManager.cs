using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.AI
{
  /// <summary> Tracks the lifetime of an object. </summary>
  internal interface IInstanceManager
  {
    /// <summary> Invoked when a new instance has been created an started. </summary>
    /// <param name="instance"> The instance that has been started. </param>
    void NotifyAlive(GameObject instance);

    /// <summary> Invoked when a new instance has been destroyed. </summary>
    /// <param name="instance"> The instance that has been destroyed. </param>
    void NotifyDestroy(GameObject instance);
  }
}