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

    private Transform _transform;
    private InventoryBehavior _inventory;
    private Transform _cameraTransform;
    private PlayerCursor _playerCursor;
    private InputActionCollection _inputAction;
    private InputActionCollection _gameControls;
    private DesiredVelocity _desiredVelocity;

    public GameObject CurrentObject { get; set; }

    public void Start()
    {
      _transform = Owner.GetComponent<Transform>();
      _inventory = Owner.GetComponent<InventoryBehavior>();
      _cameraTransform = Camera.main.GetComponent<Transform>();
      _playerCursor = GetComponentInChildren<CursorBehavior>().PlayerCursor;
      _desiredVelocity = new DesiredVelocity();

      SetMouseLock(true);

      _gameControls = new InputActionCollection()
      {
        { KeyboardEventType.ToggleDown, KeyCode.F5, ReloadLevel },
        { KeyboardEventType.ToggleDown, KeyCode.F1, PauseGame },
        { KeyboardEventType.ToggleDown, KeyCode.F2, UnpauseGame },
      };

      _inputAction = new InputActionCollection()
      {
        { KeyboardEventType.Down, KeyCode.W, () => _desiredVelocity.Forward() },
        { KeyboardEventType.Down, KeyCode.S, () => _desiredVelocity.Backward() },
        { KeyboardEventType.Down, KeyCode.A, () => _desiredVelocity.Left() },
        { KeyboardEventType.Down, KeyCode.D, () => _desiredVelocity.Right() },
        { KeyboardEventType.Down, KeyCode.Alpha1, () => _inventory.TrySwitchTo(0) },
        { KeyboardEventType.Down, KeyCode.Alpha2, () => _inventory.TrySwitchTo(1) },
        { KeyboardEventType.Down, KeyCode.Alpha3, () => _inventory.TrySwitchTo(2) },
        { KeyboardEventType.Down, KeyCode.Alpha4, () => _inventory.TrySwitchTo(3) },
        { KeyboardEventType.Down, KeyCode.Alpha5, () => _inventory.TrySwitchTo(4) },
        { KeyboardEventType.Down, KeyCode.Alpha6, () => _inventory.TrySwitchTo(5) },
        { KeyboardEventType.Down, KeyCode.Alpha7, () => _inventory.TrySwitchTo(6) },
        { KeyboardEventType.Down, KeyCode.Alpha8, () => _inventory.TrySwitchTo(7) },
        { KeyboardEventType.Down, KeyCode.Alpha9, () => _inventory.TrySwitchTo(8) },
        { KeyboardEventType.Down, KeyCode.Alpha0, () => _inventory.TrySwitchTo(9) },
        { KeyboardEventType.Down, KeyCode.R, () => _inventory.TryReload() },
        { MouseEventType.ButtonDown, MouseButton.LeftButton, () => _inventory.TryTrigger1() },
        { MouseEventType.ButtonDown, MouseButton.RightButton, () => _inventory.TryTrigger2() },
      };
    }

    private static void SetMouseLock(bool isMouseLocked)
    {
      if (isMouseLocked)
      {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
      }
      else
      {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
      }
    }

    public void Update()
    {
      _gameControls.CheckInput();

      // bail out if we're paused
      if (IsPaused)
        return;

      TrackMouse();

      _desiredVelocity.Reset();
      _inputAction.CheckInput();

      CheckMovement(_desiredVelocity.Velocity);
      DetectCurrentObject();
      CheckInventory();
      RecenterCamera();
    }

    /// <summary> True if the game is currently paused. </summary>
    private static bool IsPaused
    {
      get { return Time.timeScale == 0; }
    }

    private static void ReloadLevel()
    {
      Time.timeScale = 1;
      Application.LoadLevel(0);
    }

    private static void UnpauseGame()
    {
      Time.timeScale = 1;
      SetMouseLock(false);
    }

    private static void PauseGame()
    {
      Time.timeScale = 0;
      SetMouseLock(false);
    }

    private void RecenterCamera()
    {
      // todo make this scroll smoother
      _cameraTransform.position = _transform.position;
    }

    /// <summary> Check all input related to movement and move the player accordingly. </summary>
    private void CheckMovement(Vector3 desiredVelocity)
    {
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

      _inputAction.CheckInput();
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

    /// <summary> Holds the velocity that the user would like to move in. </summary>
    private struct DesiredVelocity
    {
      private Vector3 _desiredVelocity;

      public void Reset()
      {
        _desiredVelocity = new Vector3();
      }

      public void Forward()
      {
        _desiredVelocity.y += 1;
      }

      public void Backward()
      {
        _desiredVelocity.y -= 1;
      }

      public void Left()
      {
        _desiredVelocity.x -= 1;
      }

      public void Right()
      {
        _desiredVelocity.x += 1;
      }

      public Vector3 Velocity
      {
        get { return _desiredVelocity; }
      }
    }
  }
}