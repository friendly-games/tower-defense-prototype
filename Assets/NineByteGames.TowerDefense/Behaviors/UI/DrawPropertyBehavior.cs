using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Player;
using UnityEngine;
using UnityEngine.UI;

namespace NineByteGames.TowerDefense.Behaviors.UI
{
  /// <summary> A draw property behavior. </summary>
  [RequireComponent(typeof(Text))]
  internal class DrawPropertyBehavior : DrawPlayerInfoBehavior
  {
    protected override GameObject GetObjectToDraw()
    {
      return Player.GetComponent<PlayerInputBehavior>().CurrentObject;
    }
  }
}