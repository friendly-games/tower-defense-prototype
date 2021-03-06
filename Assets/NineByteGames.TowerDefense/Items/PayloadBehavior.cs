﻿using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Behaviors;
using NineByteGames.TowerDefense.Messages;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;

namespace NineByteGames.TowerDefense.Items
{
  /// <summary> Provides a payload for an item. </summary>
  internal class PayloadBehavior : AttachedBehavior, IReadable
  {
    public PayloadType Type;
    public int Amount;

    public enum PayloadType
    {
      Health,
      GeneralDamage,
      Money,
    }

    public void OnCollisionEnter2D(Collision2D colidee)
    {
      HandleCollision(colidee.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D colidee)
    {
      HandleCollision(colidee.gameObject);
    }

    private void HandleCollision(GameObject otherObject)
    {
      // make sure that we should be sending messages to it
      if (otherObject.GetComponent<NoMessagesBehavior>() != null)
        return;

      switch (Type)
      {
        case PayloadType.GeneralDamage:
          otherObject.SendSignal(AllSignals.Damage, new Damage(Amount));
          DestroyOwner();
          break;

        case PayloadType.Health:
          var healing = new Healing(Amount);
          otherObject.SendSignal(AllSignals.Health, healing);

          if (healing.Remaining <= 0)
          {
            DestroyOwner();
          }
          break;

        case PayloadType.Money:
          // TODO move towards target
          bool wasHandled = otherObject.SendSignal(AllSignals.MoneyTransfer, new MoneyTransfer(Amount));
          if (wasHandled)
          {
            DestroyOwner();
          }
          break;
      }
    }

    void IReadable.AddText(ReadableText builder)
    {
      builder.AddProperty("Payload-Type", Type)
             .AddProperty("Payload-Amount", Amount);
    }
  }
}