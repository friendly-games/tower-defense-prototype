using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors.Tracking
{
  internal class CollectItemByMovingBehavior : AttachedBehavior
  {
    public GameObject ObjectToTrack;
    public Layer LayerToCollect;
    public float MaxDistance = 10.0f;

    public void OnTriggerEnter2D(Collider2D other)
    {
      StartTracking(other.gameObject);
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
      StartTracking(coll.gameObject);
    }

    private void StartTracking(GameObject gameObject)
    {
      if (LayerToCollect.LayerId != gameObject.layer)
        return;

      var accelerate = gameObject.gameObject.GetComponent<AccelerateTowardsTargetBehavior>();

      if (accelerate == null)
      {
        accelerate = gameObject.gameObject.AddComponent<AccelerateTowardsTargetBehavior>();
        accelerate.StartTracking(this.gameObject.GetParent());
      }
    }
  }
}