using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.World;
using NineByteGames.TowerDefense.World.Grid;
using Pathfinding;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors.World
{
  internal static class GridUpdate
  {
    public static void ResetGraph()
    {
      var gridGraph = (GridGraph) AstarPath.active.graphs[0];

      var graphUpdateObject =
        new GraphUpdateObject(new Bounds(new Vector3(0, 0, 0), new Vector3(gridGraph.Width, gridGraph.Depth, 0)))
        {
          modifyWalkability = true,
          setWalkability = true,
        };

      AstarPath.active.UpdateGraphs(graphUpdateObject);
    }

    /// <summary> Marks the designated coordinate as unwalkable. </summary>
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

    public static void MarkWalkable(GridCoordinate lowerLeft, Size size, bool isWalkable)
    {
      const float offset = 0.1f;

      var lowerLeftVector = new Vector3(lowerLeft.X + offset, lowerLeft.Z + offset);
      var upperRightVector = new Vector3(lowerLeft.X + size.Width - offset, lowerLeft.Z + size.Height - offset);

      var vectorCenter = (lowerLeftVector + upperRightVector) / 2.0f;
      var vectorSize = upperRightVector - lowerLeftVector;

      var bounds = new Bounds(vectorCenter, vectorSize);

      Debug.Log(vectorCenter);
      Debug.Log(vectorSize);

      var graphUpdateObject = new GraphUpdateObject(bounds)
                              {
                                modifyWalkability = true,
                                setWalkability = isWalkable,
                              };

      AstarPath.active.UpdateGraphs(graphUpdateObject);
    }
  }
}