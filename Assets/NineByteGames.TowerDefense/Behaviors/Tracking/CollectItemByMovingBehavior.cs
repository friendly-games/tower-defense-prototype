using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors.Tracking
{
  /// <summary> A behavior, that when attached, target objects towards itself. </summary>
  [RequireComponent(typeof(Collider2D))]
  internal class CollectItemByMovingBehavior : AttachedBehavior
  {
    [Tooltip("The object that the items should follow when enter the given radius")]
    public GameObject ObjectToTrack;

    [Tooltip("THe layer from which items should be collected")]
    public Layer LayerToCollect;

    public void Start()
    {
      if (ObjectToTrack != null)
      {
        ObjectToTrack = Owner;
      }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
      StartTracking(other.gameObject);
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
      StartTracking(coll.gameObject);
    }

    private void StartTracking(GameObject target)
    {
      if (LayerToCollect.LayerId != target.layer)
        return;

      var accelerate = target.GetComponent<AccelerateTowardsTargetBehavior>();

      if (accelerate == null)
      {
        accelerate = target.AddComponent<AccelerateTowardsTargetBehavior>();
        accelerate.StartTracking(this.gameObject.GetParent());
      }
    }
  }
}