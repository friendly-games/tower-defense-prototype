using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Behaviors;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.Unity;
using UnityEngine;

namespace NineByteGames.TowerDefense.Items
{
  /// <summary> Moves this object towards a target in order to merge itself with the target. </summary>
  [RequireComponent(typeof(PayloadBehavior))]
  internal class MergeTowardsOtherBehavior : AttachedBehavior
  {
    private GameObject _target;
    private int _trackingTime;

    /// <summary> Initializes this object. </summary>
    /// <param name="target"> The object to move towards. </param>
    public void Initialize(GameObject target)
    {
      _target = target;
    }

    [UnityMethod]
    public void FixedUpdate()
    {
      var myTransform = Owner.transform;
      var hisTransform = _target.transform;

      var diff = myTransform.position - hisTransform.position;

      if (diff.sqrMagnitude < 1)
      {
        MergeWithTarget();
      }
      else
      {
        MoveTowards(diff, myTransform);
      }
    }

    /// <summary> Move twoards the target using diff as the direction. </summary>
    private void MoveTowards(Vector3 diff, Transform myTransform)
    {
      _trackingTime++;

      diff = Vector3.ClampMagnitude(diff, 5.0f * Time.fixedDeltaTime * (_trackingTime / 8.0f));
      myTransform.position -= diff;
    }

    /// <summary> Take the payload of the target and transfer into my payload. </summary>
    private void MergeWithTarget()
    {
      var myPayload = GetComponent<PayloadBehavior>();
      var hisPayload = _target.GetComponent<PayloadBehavior>();

      myPayload.Amount += hisPayload.Amount;
      _target.Kill();

      this.RemoveComponent();
    }
  }
}