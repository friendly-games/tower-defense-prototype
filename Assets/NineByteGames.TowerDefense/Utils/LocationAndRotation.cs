using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Player
{
  /// <summary> A combined location and rotation struct. </summary>
  public struct LocationAndRotation
  {
    public Vector3 Location;
    public Quaternion Rotation;

    public LocationAndRotation(Vector3 location, Quaternion rotation)
    {
      Location = location;
      Rotation = rotation;
    }
  }
}