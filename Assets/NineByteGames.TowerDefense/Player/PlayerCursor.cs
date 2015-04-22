using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Player
{
  /// <summary> A ranged cursor. </summary>
  internal class PlayerCursor : IPlayerCursor
  {
    private Vector3 _mouseLocation;
    private Vector3 _cursorPositionRelative;
    private readonly Transform _transform;

    /// <summary> Constructor. </summary>
    /// <param name="playerTransform"> transform of the player that this cursor belongs to. </param>
    public PlayerCursor(Transform playerTransform)
    {
      _transform = playerTransform;
    }

    /// <summary>
    ///  The maximum distance that the user can move the cursor up and down or left and right.
    /// </summary>
    public float CurrentRange { get; set; }

    /// <summary>
    ///  The cursor location, equivalent to the <see cref="MouseLocation"/> but clipped to a maximum
    ///  distance of <see cref="CurrentRange"/>.  Relative to the players current location.
    /// </summary>
    public Vector3 CursorPositionRelative
    {
      get { return _cursorPositionRelative; }
    }

    /// <summary> The position of the cursor in world space. </summary>
    public Vector3 PositionAbsolute
    {
      get { return CursorPositionRelative + _transform.position; }
    }

    /// <summary> Gets the current mouse location. </summary>
    public Vector3 MouseLocation
    {
      get { return _mouseLocation; }
    }

    /// <summary> Updates all values that are based on the mouse location. </summary>
    /// <param name="mouseLocation"> The current mouse location. </param>
    public void UpdateMouseLocation(Vector3 mouseLocation)
    {
      // relative to the player
      mouseLocation -= _transform.position;

      var diff = _mouseLocation - mouseLocation;
      _mouseLocation = mouseLocation;

      _cursorPositionRelative -= diff;

      // make sure that we don't exceed the range specified
      _cursorPositionRelative.x = Mathf.Clamp(_cursorPositionRelative.x, -CurrentRange, CurrentRange);
      _cursorPositionRelative.y = Mathf.Clamp(_cursorPositionRelative.y, -CurrentRange, CurrentRange);
    }
  }
}