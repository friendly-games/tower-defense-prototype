using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Player
{
  /// <summary> The type of keyboard event that triggers an action. </summary>
  public enum KeyboardEventType
  {
    Down,
    ToggleDown,
    ToggleUp,
    Mouse,
  }

  /// <summary> The type of mouse event that triggers an action. </summary>
  public enum MouseEventType
  {
    ButtonDown,
  }

  /// <summary> The button for the mouse that triggers the action. </summary>
  public enum MouseButton
  {
    LeftButton = 0,
    RightButton = 1,
  }

  /// <summary>
  ///  Contains associations between keyboard/mouse input and the actions that occur when the
  ///  corresponding key/mouse event occurs.
  /// </summary>
  public class InputActionCollection : IEnumerable
  {
    private readonly List<IInputEventAction> _actions = new List<IInputEventAction>();

    /// <summary> Adds an action that should occur when the key is down. </summary>
    /// <param name="type"> The type of keyboard event that causes the action to be invoked. </param>
    /// <param name="key"> The key that should be checked. </param>
    /// <param name="action"> The action to perform if the key is down. </param>
    public void Add(KeyboardEventType type, KeyCode key, Action action)
    {
      switch (type)
      {
        case KeyboardEventType.Down:
          _actions.Add(new KeyDownAction(key, action));
          break;
        case KeyboardEventType.ToggleDown:
          _actions.Add(new KeyPressAction(key, action));
          break;
        case KeyboardEventType.ToggleUp:
          _actions.Add(new KeyReleaseAction(key, action));
          break;
        default:
          throw new ArgumentOutOfRangeException("type", type, null);
      }
    }

    /// <summary> Adds an action that should occur when the key is down. </summary>
    /// <param name="type"> The type of mouse event that causes the action to be invoked. </param>
    /// <param name="key"> The key that should be checked. </param>
    /// <param name="action"> The action to perform if the key is down. </param>
    public void Add(MouseEventType type, MouseButton key, Action action)
    {
      switch (type)
      {
        case MouseEventType.ButtonDown:
          _actions.Add(new MouseDownEventAction(key, action));
          break;
        default:
          throw new ArgumentOutOfRangeException("type", type, null);
      }
    }

    /// <summary>
    ///  Check all of the input events and trigger any associated actions that need to be invoked.
    /// </summary>
    public void CheckInput()
    {
      foreach (var action in _actions)
      {
        action.PerformCheck();
      }
    }

    public IEnumerator GetEnumerator()
    {
      throw new NotImplementedException();
    }

    /// <summary> An action that occurs when input from the user is received. </summary>
    internal interface IInputEventAction
    {
      void PerformCheck();
    }

    private class KeyDownAction : IInputEventAction
    {
      private readonly KeyCode _key;
      private readonly Action _action;

      public KeyDownAction(KeyCode key, Action action)
      {
        _key = key;
        _action = action;
      }

      public void PerformCheck()
      {
        if (Input.GetKey(_key))
        {
          _action.Invoke();
        }
      }
    }

    public class KeyPressAction : IInputEventAction
    {
      private readonly KeyCode _key;
      private readonly Action _action;

      public KeyPressAction(KeyCode key, Action action)
      {
        _key = key;
        _action = action;
      }

      public void PerformCheck()
      {
        if (Input.GetKeyDown(_key))
        {
          _action.Invoke();
        }
      }
    }

    public class KeyReleaseAction : IInputEventAction
    {
      private readonly KeyCode _key;
      private readonly Action _action;

      public KeyReleaseAction(KeyCode key, Action action)
      {
        _key = key;
        _action = action;
      }

      public void PerformCheck()
      {
        if (Input.GetKeyUp(_key))
        {
          _action.Invoke();
        }
      }
    }

    private class MouseDownEventAction : IInputEventAction
    {
      private readonly int _mouseButton;
      private readonly Action _action;

      public MouseDownEventAction(MouseButton mouseButton, Action action)
      {
        _mouseButton = (int)mouseButton;
        _action = action;
      }

      public void PerformCheck()
      {
        if (Input.GetMouseButton(_mouseButton))
        {
          _action.Invoke();
        }
      }
    }
  }
}