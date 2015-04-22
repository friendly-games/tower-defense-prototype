using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;

namespace NineByteGames.TowerDefense.Player
{
  /// <summary> Determines various properties regarding the player's reach </summary>
  internal class CursorBehavior : AttachedBehavior
  {
    #region Unity Properties

    [Tooltip("The range from the player where items can be placed")]
    public float Range = 10.0f;

    #endregion

    /// <summary> Controls where the player is currently pointing to. </summary>
    public PlayerCursor PlayerCursor { get; private set; }

    public void Awake()
    {
      PlayerCursor = new PlayerCursor(Owner.GetParent().GetComponent<Transform>())
                     {
                       CurrentRange = Range
                     };
    }

    public void Update()
    {
      this.transform.position = PlayerCursor.PositionAbsolute;
    }
  }
}