using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Behaviors.UI;
using NineByteGames.TowerDefense.Messages;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;

namespace NineByteGames.TowerDefense.Player
{
  internal class PlayerDeathBehavior : ChildBehavior, ISignalListener<SignalIndicators.DeathIndicator>
  {
    #region Implementation of ISignalListener<DeathIndicator>

    [SignalPriority(SignalPriorities.VeryHigh)]
    public SignalListenerResult Handle(SignalIndicators.DeathIndicator message)
    {
      var screens = GameObject.Find("Global UI").GetComponent<UiPanelsBehavior>();
      screens.Mode = UiPanelsBehavior.UiMode.Dead;

      Time.timeScale = 0;

      return SignalListenerResult.StopProcessing;
    }

    #endregion
  }
}