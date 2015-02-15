using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.Common.Structures;
using NineByteGames.TowerDefense.World.Grid;

namespace NineByteGames.TowerDefense.World
{
  /// <summary>
  ///  Determines the associated cost and direction to move in when moving towards a specific target
  ///  on the map.
  /// </summary>
  /// <example>
  ///  <code>
  ///    A0XXX0
  ///    000X00
  ///    000000
  ///    XXXX0B
  ///  </code>
  ///  Would generate a flow grid similar to below to get from B to A.
  ///  <code>
  ///    A1XXX7
  ///    112X56
  ///    223456
  ///    XXXX6B
  ///  </code>
  /// </example>
  internal class GridTargetFlow
  {
    private readonly WorldGrid _world;
    private readonly int _halfWidth;
    private Array2D<FlowData> _data;
    private readonly Array2D<CellData> _cachedView;
    private readonly Queue<GridCoordinate> _reanalyzeQueue;

    /// <summary> Constructor. </summary>
    /// <param name="world"> The world from which tiles should be loaded. </param>
    /// <param name="halfWidth"> How far from the center tile that the flow should analyze.  Double
    ///  this value to get the width (or height) of the box that this object analyzes. </param>
    public GridTargetFlow(WorldGrid world, int halfWidth)
    {
      _world = world;
      _halfWidth = halfWidth;

      // + 1 because we want an odd number so that our "target" is directly in the middle of our grid
      _cachedView = new Array2D<CellData>(halfWidth * 2 + 1, halfWidth * 2 + 1);
      _data = new Array2D<FlowData>(_cachedView.Width, _cachedView.Width);

      _reanalyzeQueue = new Queue<GridCoordinate>();
    }

    /// <summary> Reanalyze the grid to determine the directions and costs for moving towards the given coordinate. </summary>
    /// <param name="coordinate"> The coordinate where the flow should move towards. </param>
    public void ReanalyzeFor(GridCoordinate coordinate)
    {
      var upperLeft = new GridCoordinate(coordinate.X - _halfWidth, coordinate.Z - _halfWidth);
      _world.Get(upperLeft, _cachedView);

      _reanalyzeQueue.Clear();
      _reanalyzeQueue.Enqueue(new GridCoordinate(_halfWidth, _halfWidth));
    }

    private void EnqueueSibilings(GridCoordinate coordinate)
    {
      // bottomLeft
    }

    private struct FlowData
    {
      public short LowestCost;
      public short Flow;
    }
  }
}