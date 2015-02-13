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

    public void Awake()
    {
      _currentTargets = new LinkedList<GameObject>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
      if (!Layer.FromLayerId(other.gameObject.layer).IsIn(CollisionLayer))
        return;

      _currentTargets.AddLast(other.gameObject);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
      if (!Layer.FromLayerId(other.gameObject.layer).IsIn(CollisionLayer))
        return;

      _currentTargets.Remove(other.gameObject);
    }

    public void Update()
    {
      // remove nodes that have died
      var node = _currentTargets.First;

      while (node != null)
      {
        var next = node.Next;
        if (node.Value.IsDead())
        {
          _currentTargets.Remove(node);
        }
        node = next;
      }

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

      var currentCloset = _currentTargets
        .MinBy(other => (myPosition - other.transform.position).sqrMagnitude);

      if (currentCloset != _lastTarget)
      {
        _lastTarget = currentCloset;
        Send(new TargetAquiredSignal(currentCloset));
      }
    }
  }
}