using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Tasks
{
  /// <summary> A unit of work that can be processed on another thread. </summary>
  internal interface ITask
  {
    /// <summary> Executes the given task. </summary>
    void Execute();
  }
}