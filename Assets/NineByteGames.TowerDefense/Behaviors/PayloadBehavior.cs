using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NineByteGames.TowerDefense.Messages;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors
{
  /// <summary> Provides a payload for an item. </summary>
  internal class PayloadBehavior : AttachedBehavior, IReadable
  {
    public PayloadType Type;
    public float Amount;
    private PayloadBehavior _payload;

    public enum PayloadType
    {
      Health,
      GeneralDamage,
      Money,
    }

    public override void Start()
    {
      base.Start();

      _payload = this;
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
      switch (_payload.Type)
      {
        case PayloadType.GeneralDamage:
          otherObject.SendSignal(new Damage(_payload.Amount));
          DestroyOwner();
          break;

        case PayloadType.Health:

          var healing = new Healing(_payload.Amount);
          otherObject.SendSignal(healing);

          if (healing.Remaining <= 0)
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