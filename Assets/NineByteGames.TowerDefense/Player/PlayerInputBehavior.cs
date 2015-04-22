using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;

namespace NineByteGames.TowerDefense.Player
{
  internal class PlayerInputBehavior : AttachedBehavior, IUpdatable
  {
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

    private Transform _transform;
    private InventoryBehavior _inventory;
    private Transform _cameraTransform;
    private PlayerCursor _playerCursor;

    public GameObject CurrentObject { get; set; }

    public void Start()
    {
      _transform = Owner.GetComponent<Transform>();
      _inventory = Owner.GetComponent<InventoryBehavior>();
      _cameraTransform = FindObjectOfType<Camera>().GetComponent<Transform>();
      _playerCursor = GetComponentInChildren<CursorBehavior>().PlayerCursor;

      Cursor.visible = false;
      Cursor.lockState = CursorLockMode.Confined;
    }

    public void Update()
    {
      TrackMouse();
      CheckMovement();
      DetectCurrentObject();
      CheckInventory();
      RecenterCamera();
    }

    private void RecenterCamera()
    {
      // todo make this scroll smoother
      _cameraTransform.position = _transform.position;
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

      _transform.GetComponent<Rigidbody2D>().velocity = new Vector2();
    }

    private void CheckInventory()
    {
      _playerCursor.UpdateMouseLocation(Camera.main.ScreenToWorldPoint(Input.mousePosition));

      if (Input.GetKey(KeyCode.Alpha1))
      {
        _inventory.TrySwitchTo(0);
      }
      else if (Input.GetKey(KeyCode.Alpha2))
      {
        _inventory.TrySwitchTo(1);
      }
      else if (Input.GetKey(KeyCode.Alpha3))
      {
        _inventory.TrySwitchTo(2);
      }
      else if (Input.GetKey(KeyCode.Alpha4))
      {
        _inventory.TrySwitchTo(3);
      }

      if (Input.GetMouseButton(0))
      {
        _inventory.TryTrigger1();
      }

      if (Input.GetMouseButton(1))
      {
        _inventory.TryTrigger2();
      }
    }

    private void TrackMouse()
    {
      Quaternion desired = Quaternion.Lerp(
        transform.rotation,
        Quaternion.LookRotation(Vector3.forward, _playerCursor.PositionAbsolute - transform.position),
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