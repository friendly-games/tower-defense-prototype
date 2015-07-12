using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;

namespace NineByteGames.TowerDefense.Items
{
  [RequireComponent(typeof(CircleCollider2D))]
  internal class RadiusBasedOnPayloadBehavior : SignalReceiverBehavior<RadiusBasedOnPayloadBehavior>
  {
    private Transform _root;
    private PayloadBehavior _payload;

    static RadiusBasedOnPayloadBehavior()
    {
      SignalEntryPoint.For<RadiusBasedOnPayloadBehavior>()
                      .Register(AllSignals.Merged, (i,d) => i.HandleMerged(), AllPriorities.VeryLow);
    }

    public override void Start()
    {
      base.Start();

      _payload = GetComponent<PayloadBehavior>();
      _root = FindRootParent().GetComponent<Transform>();
    }

    private void HandleMerged()
    {
      float expectedSize = 1 + Mathf.Max(Mathf.Log(Mathf.Log10(_payload.Amount / 100f)), 0);
      _root.localScale = new Vector3(expectedSize, expectedSize, expectedSize);
    }
  }
}