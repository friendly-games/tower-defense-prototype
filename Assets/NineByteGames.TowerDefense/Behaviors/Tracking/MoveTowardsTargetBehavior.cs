using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Messages;
using NineByteGames.TowerDefense.Signals;
using Pathfinding;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors.Tracking
{
  /// <summary> Move towards the current object. </summary>
  internal class MoveTowardsTargetBehavior : ChildBehavior
  {
    [Tooltip("The speed at which the object moves towards its target")]
    public float Speed = 1.0f;

    private EntityTrackerBehavior _tracker;
    private Path _path;
    private Vector2 _lastPosition;
    private int _numTimesStuck;

    public override void Start()
    {
      base.Start();

      _tracker = GetComponent<EntityTrackerBehavior>();
      _seeker = GetComponent<Seeker>();

      _seeker.pathCallback += HandlePathUpdate;
    }

    private int _currentPathCount = 0;

    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;

    private Seeker _seeker;
    private bool _pathPending;

    public void FixedUpdate()
    {
      if (_path == null)
      {
        UpdatePath();
        return;
      }

      if (_currentPathCount >= 60)
      {
        UpdatePath();
        return;
      }

      if (currentWaypoint >= _path.vectorPath.Count)
        return;

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
            ray.collider.gameObject.SendSignal(new Damage(10));
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
      var target = _tracker.Target;
      if (target == null || _pathPending)
        return;

      Debug.Log("New Path Requested");

      var seeker = GetComponent<Seeker>();
      var location = GetComponent<Transform>();
      seeker.StartPath(location.position, target.GetComponent<Transform>().position);
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