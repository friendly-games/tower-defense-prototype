using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Messages;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.Utils;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors.Tracking
{
  [RequireComponent(typeof(Rigidbody2D))]
  internal class RadiusBasedTargeter : ChildBehavior
  {
    /// <summary> The layers that will be targeted. </summary>
    public LayerMask CollisionLayer;

    private LinkedList<GameObject> _currentTargets;
    private GameObject _lastTarget;

    public override void Start()
    {
      base.Start();

      _currentTargets = new LinkedList<GameObject>();
    }

    public void OnTriggerStay2D(Collider2D other)
    {
      if (!Layer.FromLayerId(other.gameObject.layer).IsIn(CollisionLayer))
        return;

      // add it to the current list of items in the trigger area
      _currentTargets.AddLast(other.gameObject);
    }

    public void FixedUpdate()
    {
      // if we have no targets, do a quick exit
      if (_currentTargets.Count == 0)
      {
        // if we had a target before, then we lost the target
        if (!_lastTarget.IsNull())
        {
          _lastTarget = null;
          Send(TargetAquiredSignal.TargetLost);
        }

        return;
      }

      var myPosition = RootBehavior.transform.position;

      var currentCloset = _currentTargets.MinBy(other => (myPosition - other.transform.position).sqrMagnitude);

      if (currentCloset != _lastTarget)
      {
        _lastTarget = currentCloset;
        Send(new TargetAquiredSignal(currentCloset));
      }

      // make sure that during the next iteration, OnTriggerStay2D adds to an empty list
      _currentTargets.Clear();
    }
  }
}