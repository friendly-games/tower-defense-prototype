using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Player
{
  /// <summary> Determines where the player is looking. </summary>
  internal interface IPlayerCursor
  {
    /// <summary>
    ///  The cursor location, relative to the players current location.
    /// </summary>
    Vector3 CursorPositionRelative { get; }

    /// <summary> The position of the cursor in world space. </summary>
    Vector3 PositionAbsolute { get; }
  }
}