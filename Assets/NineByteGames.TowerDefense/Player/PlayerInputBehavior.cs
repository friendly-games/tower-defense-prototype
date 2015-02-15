﻿using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;

namespace NineByteGames.TowerDefense.Player
{
  internal class PlayerInputBehavior : AttachedBehavior, IUpdatable
  {
    /// <summary> The object that should be created when right clicking. </summary>
    public GameObject EnemyCreator;

    /// <summary> How fast the player moves </summary>
    public float PlayerSpeed = 2.25f;

    /// <summary> How fast the player turns. </summary>
    public int TurnSpeed = 4;

    /// <summary> All of the layers that should be displayed. </summary>
    public LayerMask InfoLayerMask;

    private const KeyCode KeyUp = KeyCode.W;
    private const KeyCode KeyDown = KeyCode.S;
    private const KeyCode KeyLeft = KeyCode.A;
    private const KeyCode KeyRight = KeyCode.D;

    private Collider2D _physics;
    private Transform _transform;
    private InventoryBehavior _inventory;

    public GameObject CurrentObject { get; set; }

    public void Start()
    {
      _physics = Owner.GetComponent<Collider2D>();
      _transform = Owner.GetComponent<Transform>();
      _inventory = Owner.GetComponent<InventoryBehavior>();
    }

    public void Update()
    {
      TrackMouse();
      CheckMovement();
      DetectCurrentObject();

      if (Input.GetMouseButton(0) && _inventory.CanTrigger1)
      {
        _inventory.Trigger1();
      }

      _inventory.UpdateCursor(Camera.main.ScreenToWorldPoint(Input.mousePosition));


      if (Input.GetMouseButton(1) && _inventory.CanTrigger2)
      {
        _inventory.Trigger2();
      }
    }

    /// <summary> Check all input related to movement and move the player accordingly. </summary>
    private void CheckMovement()
    {
      var desiredVelocity = new Vector3();

      if (Input.GetKey(KeyUp))
      {
        desiredVelocity.y += 1;
      }

      if (Input.GetKey(KeyDown))
      {
        desiredVelocity.y -= 1;
      }

      if (Input.GetKey(KeyLeft))
      {
        desiredVelocity.x -= 1;
      }

      if (Input.GetKey(KeyRight))
      {
        desiredVelocity.x += 1;
      }

      if (desiredVelocity.sqrMagnitude > 0.001f)
      {
        desiredVelocity = desiredVelocity.normalized;
        _transform.position += desiredVelocity * Time.deltaTime * PlayerSpeed;
      }

      _transform.rigidbody2D.velocity = new Vector2();
    }

    private void TrackMouse()
    {
      Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

      Quaternion desired = Quaternion.Lerp(transform.rotation,
                                           Quaternion.LookRotation(Vector3.forward, mousePos - transform.position),
                                           Time.deltaTime * TurnSpeed);

      transform.rotation = desired;
    }

    private void DetectCurrentObject()
    {
      // Fire a ray from the camera into the world
      Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      position.z -= 1;

      var ray = new Ray(position, new Vector3(0, 0, 1));

      // Test it out to 1000 units. If it hits something, continue.
      RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),
                                           Vector2.zero,
                                           100.0f,
                                           InfoLayerMask.value);

      CurrentObject = hit.collider != null
        ? hit.collider.gameObject
        : null;
    }
  }
}