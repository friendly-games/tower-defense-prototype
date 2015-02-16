using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.Common.Structures;
using NineByteGames.TowerDefense.Behaviors.World;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.World;
using NineByteGames.TowerDefense.World.Grid;
using UnityEngine;

namespace NineByteGames.TowerDefense.Objects
{
  internal class InitializeObjectInWorldBehavior : AttachedBehavior
  {
    [Range(1, 5)]
    [Tooltip("The number of spaces that this element takes up")]
    public int Width = 1;

    [Range(1, 5)]
    [Tooltip("The number of spaces that this element takes up")]
    public int Height = 1;

    [Tooltip("The type of the element in the world")]
    public int Type;

    public void Start()
    {
      var position = GetComponent<Transform>().position;
      var lowerLeft = position - (new Vector3(Width, Height, 0) / 2.0f);

      // move into the center of the square
      lowerLeft += new Vector3(0.5f, 0.5f);

      GridUpdate.MarkWalkable(GridCoordinate.FromVector3(lowerLeft), new Size(Width, Height), false);
    }
  }
}