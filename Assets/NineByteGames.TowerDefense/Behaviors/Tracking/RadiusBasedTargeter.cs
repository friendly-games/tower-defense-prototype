using NineByteGames.Common.Extensions;
using NineByteGames.TowerDefense.Messages;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
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
    private GameObject _rootParent;

    [UnityMethod]
    public void Awake()
    {
      _currentTargets = new LinkedList<GameObject>();
    }

    [UnityMethod]
    public override void Start()
    {
      base.Start();

      _rootParent = FindRootParent();
    }

    [UnityMethod]
    public void OnTriggerEnter2D(Collider2D other)
    {
      if (!Layer.FromLayerId(other.gameObject.layer).IsIn(CollisionLayer))
        return;

      _currentTargets.AddLast(other.gameObject);
    }

    [UnityMethod]
    public void OnTriggerExit2D(Collider2D other)
    {
      if (!Layer.FromLayerId(other.gameObject.layer).IsIn(CollisionLayer))
        return;

      _currentTargets.Remove(other.gameObject);
    }

    [UnityMethod]
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

      var myPosition = _rootParent.transform.position;

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