using NineByteGames.TowerDefense.AI;
using NineByteGames.TowerDefense.Messages;
using NineByteGames.TowerDefense.Signals;
using Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors.Tracking
{
  /// <summary> Move towards the current object. </summary>
  internal class MoveTowardsTargetBehavior : SignalReceiverBehavior<MoveTowardsTargetBehavior>
  {
    static MoveTowardsTargetBehavior()
    {
      SignalEntryPoint.For<MoveTowardsTargetBehavior>()
                      .Register(AllSignals.TargetChanged, (i, d) => i.HandleTargetChanged(d));
    }

    [Tooltip("The speed at which the object moves towards its target")]
    public float Speed = 1.0f;

    [Tooltip("The target to move towards")]
    public GameObject Target;

    private Path _path;
    private Vector2 _lastPosition;
    private int _numTimesStuck;
    private int _currentPathCount = 0;

    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;

    private Seeker _seeker;
    private bool _pathPending;

    public override void Start()
    {
      base.Start();

      _seeker = GetComponent<Seeker>();

      _seeker.pathCallback += HandlePathUpdate;
    }

    void HandleTargetChanged(TargetAquiredSignal message)
    {
      Target = message.Target;
      enabled = message.Target != null;
    }

    public void FixedUpdate()
    {
      // if we've travelled on this path for too long or if we're past the last waypoint, reset. 
      if (_path == null
          || _currentPathCount >= 60
          || currentWaypoint >= _path.vectorPath.Count)
      {
        UpdatePath();
        return;
      }

      //Direction to the next waypoint
      Vector3 dir = (_path.vectorPath[currentWaypoint] - transform.position).normalized;
      dir *= Time.fixedDeltaTime * 2;

      var body = GetComponent<Rigidbody2D>();
      body.position += new Vector2(dir.x, dir.y);

      // if we didn't move far from last time
      if ((body.position - _lastPosition).sqrMagnitude < 0.05 * Time.fixedDeltaTime)
      {
        if (_numTimesStuck > 20)
        {
          var ray = Physics2D.Raycast(body.position, dir, 1.0f, Layer.FromName("Buildings").LayerMaskValue);
          if (ray.collider != null)
          {
            Debug.Log("Hit:" + ray.collider.gameObject);
            // TODO
            ray.collider.gameObject.SendSignal(AllSignals.Damage, new Damage(10));
          }
        }
        else
        {
          _numTimesStuck++;
        }
      }
      else
      {
        _numTimesStuck = 0;
      }

      _lastPosition = body.position;

      _currentPathCount++;

      //Check if we are close enough to the next waypoint
      //If we are, proceed to follow the next waypoint
      if (Vector3.Distance(transform.position, _path.vectorPath[currentWaypoint]) < 0.1f)
      {
        currentWaypoint++;
      }
    }

    private void UpdatePath()
    {
      if (Target == null)
        return;
      if (_pathPending)
        return;

      var target = Target.transform;

      var seeker = GetComponent<Seeker>();
      var location = GetComponent<Transform>();
      seeker.StartPath(location.position, target.position);
      _pathPending = true;
    }

    private void HandlePathUpdate(Path p)
    {
      _path = p;
      currentWaypoint = 0;
      _currentPathCount = 0;
      _pathPending = false;
    }
  }
}