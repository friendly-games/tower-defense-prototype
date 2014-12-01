using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Behaviors.UI;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace NineByteGames.TowerDefense.Behaviors
{
  /// <summary> A draw property behavior. </summary>
  [RequireComponent(typeof(Text))]
  internal class DrawPropertyBehavior : DrawPlayerInfoBehavior
  {
    protected override GameObject GetObjectToDraw()
    {
      return Player.GetComponent<PlayerInput>().CurrentObject;
    }
  }
}