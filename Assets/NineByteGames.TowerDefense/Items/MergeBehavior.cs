using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.Common.Extensions;
using NineByteGames.TowerDefense.Behaviors;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.Unity;
using NineByteGames.TowerDefense.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NineByteGames.TowerDefense.Items
{
  /// <summary>
  ///  Finds similar objects to this object and adds a <seealso cref="MergeTowardsOtherBehavior"/>
  ///  to move this object towards the other object.
  /// </summary>
  [RequireComponent(typeof(Rigidbody2D))]
  [RequireComponent(typeof(PayloadBehavior))]
  internal class MergeBehavior : AttachedBehavior, INotifyOnDestruction
  {
    private ObjectTracker<MergeBehavior> _tracker;
    private PayloadBehavior.PayloadType _type;

    [UnityMethod]
    public void Awake()
    {
      _tracker = new ObjectTracker<MergeBehavior>(this, OnChanged);
    }

    [UnityMethod]
    public void Start()
    {
      _type = GetComponent<PayloadBehavior>().Type;
    }

    [UnityMethod]
    public void OnTriggerEnter2D(Collider2D collision)
    {
      var other = collision.gameObject.GetComponent<MergeBehavior>();
      if (other == null)
        return;
      if (other.GetComponent<PayloadBehavior>().Type != _type)
        return;

      _tracker.StartTracking(other);
    }

    [UnityMethod]
    public void OnTriggerExit2D(Collider2D collision)
    {
      var other = collision.gameObject.GetComponent<MergeBehavior>();
      if (other == null)
        return;
      if (other.GetComponent<PayloadBehavior>().Type != _type)
        return;

      _tracker.StopTracking(other);
    }

    private void OnChanged()
    {
      if (_tracker.Count > 0)
      {
        var target = GetBestTarget(this.transform.position);
        GetOrAddComponent<MergeTowardsOtherBehavior>().Initialize(target.Owner);
      }
      else
      {
        var mergeTowards = GetComponent<MergeTowardsOtherBehavior>();
        if (mergeTowards != null)
        {
          mergeTowards.RemoveComponent();
        }
      }
    }

    /// <summary> Find the best MergeTarget to go after, closest target wins. </summary>
    private MergeBehavior GetBestTarget(Vector3 currentPosition)
    {
      return _tracker.MinBy(other => (other.transform.position - currentPosition).sqrMagnitude);
    }

    [UnityMethod]
    public void OnDestroy()
    {
      if (Destroyed != null)
      {
        Destroyed.Invoke(this);
      }
      _tracker.Dispose();

      Destroyed = null;
    }

    /// <summary> Invoked when the object is destroyed. </summary>
    public event Action<Object> Destroyed;
  }
}