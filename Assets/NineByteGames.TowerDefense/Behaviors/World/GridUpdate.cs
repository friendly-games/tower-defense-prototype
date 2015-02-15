﻿using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.World;
using NineByteGames.TowerDefense.World.Grid;
using Pathfinding;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors.World
{
  /// <summary> Provides helper methods for updating the A* grid for pathfinding. </summary>
  internal static class GridUpdate
  {
    /// <summary> Clear the graph so that no areas are marked as unwalkable. </summary>
    public static void ResetGraph()
    {
      var gridGraph = (GridGraph) AstarPath.active.graphs[0];

      MarkWalkable(new GridCoordinate(0, 0), new Size(gridGraph.Width, gridGraph.Depth), true);
    }

    /// <summary>
    ///  Marks the designated coordinate as walkable or unwalkable depending on the value of
    ///  <paramref name="isWalkable"/>.
    /// </summary>
    /// <param name="coordinate"> The grid position which should be marked as walkable/unwalkable. </param>
    /// <param name="isWalkable"> True if the position should be walkable, false otherwise. </param>
    public static void MarkWalkable(GridCoordinate coordinate, bool isWalkable)
    {
      var bounds = new Bounds(coordinate.ToVector3(), new Vector3(0.5f, 0.5f, 0));
      var graphUpdateObject = new GraphUpdateObject(bounds)
                              {
                                modifyWalkability = true,
                                setWalkability = isWalkable,
                              };

      AstarPath.active.UpdateGraphs(graphUpdateObject);
    }

    /// <summary>
    ///  Marks the designated coordinate as walkable or unwalkable depending on the value of
    ///  <paramref name="isWalkable"/>.
    /// </summary>
    /// <param name="lowerLeft"> The lower left of the area which should be marked as
    ///  walkable/unwalkable. </param>
    /// <param name="size"> The size of the area that should be marked walkable or unwalkable. </param>
    /// <param name="isWalkable"> True if the position should be walkable, false otherwise. </param>
    public static void MarkWalkable(GridCoordinate lowerLeft, Size size, bool isWalkable)
    {
      const float offset = 0.1f;

      // calculate the lower left and upper left, but bring in both sides by .1f; this is so that we
      // won't accidentally include grid nodes next to us that might be accidentally included due to
      // rounding errors. 
      var lowerLeftVector = new Vector3(lowerLeft.X + offset, lowerLeft.Z + offset);
      var upperRightVector = new Vector3(lowerLeft.X + size.Width - offset, lowerLeft.Z + size.Height - offset);

      var vectorCenter = (lowerLeftVector + upperRightVector) / 2.0f;
      var vectorSize = upperRightVector - lowerLeftVector;

      var bounds = new Bounds(vectorCenter, vectorSize);

      var graphUpdateObject = new GraphUpdateObject
                              {
                                bounds = bounds,
                                modifyWalkability = true,
                                setWalkability = isWalkable,
                              };

      AstarPath.active.UpdateGraphs(graphUpdateObject);
    }
  }
}