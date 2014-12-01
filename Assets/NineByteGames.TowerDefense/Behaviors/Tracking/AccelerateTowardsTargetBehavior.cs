using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors.Tracking
{
  /// <summary> Continually accelerates a single object towards another until it reaches it. </summary>
  internal class AccelerateTowardsTargetBehavior : AttachedBehavior
  {
    private Transform _currentLocation;
    private Transform _targetLocation;
    private Rigidbody2D _rigidBody;
    private int _tickCount;

    public void StartTracking(GameObject target)
    {
      _currentLocation = GetComponent<Transform>();
      _targetLocation = target.GetComponent<Transform>();

      _rigidBody = GetComponent<Rigidbody2D>();

      // a rigid body is needed for forces
      if (_rigidBody == null)
      {
        _rigidBody = gameObject.AddComponent<Rigidbody2D>();
      }
    }

    public void FixedUpdate()
    {
      _tickCount++;

      var diff = _targetLocation.position - _currentLocation.position;
      diff.z = 0;
      diff = Vector3.ClampMagnitude(diff, Mathf.Sqrt(_tickCount));

      _rigidBody.AddForce(new Vector2(diff.x, diff.y), ForceMode2D.Impulse);
    }
  }
}