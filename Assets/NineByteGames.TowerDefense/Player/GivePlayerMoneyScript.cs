using System;
using System.Collections;
using System.Collections.Generic;
using NineByteGames.TowerDefense.Messages;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;

namespace NineByteGames.TowerDefense.Player
{
  /// <summary> Gives the player money as part of a startup script. </summary>
  internal class GivePlayerMoneyScript : AttachedBehavior
  {
    public int m_Amount;

    public GameObject m_Player;

    public void Start()
    {
      m_Player.SendFutureSignal(AllSignals.MoneyTransfer, new MoneyTransfer(m_Amount));
      this.DestroyOwner();
    }
  }
}