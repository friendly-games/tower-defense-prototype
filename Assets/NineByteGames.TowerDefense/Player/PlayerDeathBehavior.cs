using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Behaviors.UI;
using NineByteGames.TowerDefense.Messages;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;

namespace NineByteGames.TowerDefense.Player
{
  internal class PlayerDeathBehavior : ChildBehavior, ISignalListener<DeathIndicator>
  {
    [SignalPriority(SignalPriorities.VeryHigh)]
    public void Handle(DeathIndicator message)
    {
      var screens = GameObject.Find("Global UI").GetComponent<UiPanelsBehavior>();
      screens.Mode = UiPanelsBehavior.UiMode.Dead;

      // pause the game
      Time.timeScale = 0;

      SignalOptions.Current.StopProcessing();
    }
  }
}