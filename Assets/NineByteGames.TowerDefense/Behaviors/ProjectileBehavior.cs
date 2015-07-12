using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Items;
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

      clone.GetComponent<Rigidbody2D>().velocity = transform.up * clone.InitialSpeed;
      clone.GetComponent<Transform>().position = transform.position + transform.up;
      clone.gameObject.layer = layer.LayerId;
    }
  }
}