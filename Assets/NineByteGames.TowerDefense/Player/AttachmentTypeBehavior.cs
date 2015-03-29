using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;

namespace NineByteGames.TowerDefense.Player
{
  /// <summary> Determines the type of attachment the current item represents. </summary>
  public class AttachmentTypeBehavior : AttachedBehavior
  {
    [Tooltip("The type of attachment point this object represents")]
    public AttachmentPoint Type;
  }
}