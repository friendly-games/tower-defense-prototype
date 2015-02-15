using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Behaviors.World;
using NineByteGames.TowerDefense.Objects;
using NineByteGames.TowerDefense.Towers;
using NineByteGames.TowerDefense.World;

namespace NineByteGames.TowerDefense.Services
{
  /// <summary> Provides access to all of the managers in the world. </summary>
  internal static class Managers
  {
    /// <summary> The terrain manager for the system. </summary>
    public static TerrainManager Terrain { get; internal set; }

    internal static TowerManager Towers { get; set; }

    internal static ObjectWorldPlacement Placer { get; set; }
  }
}