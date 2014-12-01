using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors
{
  /// <summary> Provides the properties and behavior for projectile-based entities. </summary>
  [RequireComponent(typeof(PayloadBehavior))]
  [RequireComponent(typeof(Rigidbody2D))]
  public class ProjectileBehavior : AttachedBehavior
  {
    public float InitialSpeed = 15;
    public float TimeToLive = 5;

    private Transform _transform;
    private Rigidbody2D _body;

    public void Awake()
    {
      _transform = GetComponent<Transform>();
      _body = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
      if (name != "Bullet")
      {
        DestroyOwner(TimeToLive);
      }
    }

    public void CreateAndInitializeFrom(Transform transform, Layer layer)
    {
      var clone = this.ReplicateGameObject();
      clone.Initialize(transform, layer);
    }

    private void Initialize(Transform transform, Layer layer)
    {
      _body.velocity = transform.up * InitialSpeed;
      _transform.position = transform.position + transform.up;
      gameObject.layer = layer.LayerId;
    }
  }
}