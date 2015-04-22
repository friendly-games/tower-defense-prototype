using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.General
{
  /// <summary> An item that has a name in the world. </summary>
  internal interface INamedObject
  {
    /// <summary> The name of the item. </summary>
    string Name { get; }
  }
}