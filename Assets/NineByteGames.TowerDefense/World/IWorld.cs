using System;
using System.Collections.Generic;
using NineByteGames.TowerDefense.Buildings;

namespace NineByteGames.TowerDefense.World
{
  /// <summary> Classes that should be shared among the world. </summary>
  internal interface IWorld
  {
    /// <summary> The building placer for the world. </summary>
    BuildingWorldPlacement Placer { get; }
  }
}