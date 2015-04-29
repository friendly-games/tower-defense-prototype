using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors.UI
{
  /// <summary> Contains all of the panels that are displayed during a scene. </summary>
  internal class UiPanelsBehavior : AttachedBehavior
  {
    #region Unity Properties

    [Tooltip("The default view shown to the user on startup.  Contains inventory display etc.")]
    public GameObject DefaultView;

    [Tooltip("Screen displayed when the user dies")]
    public GameObject DeadView;

    #endregion

    private UiMode _mode;
    private GameObject _activeScreen;

    public void Start()
    {
      var mainCamera = Camera.main;

      DefaultView.GetComponent<Canvas>().worldCamera = mainCamera;
      DeadView.GetComponent<Canvas>().worldCamera = mainCamera;

      // make invalid so that when we set the property value, it is registered as a change
      _mode = (UiMode)int.MaxValue;
      _activeScreen = DeadView;

      Mode = UiMode.Default;
    }

    private void SetActiveScreen(GameObject gameObject)
    {
      if (_activeScreen == gameObject)
        return;

      _activeScreen.SetActive(false);
      _activeScreen = gameObject;
      _activeScreen.SetActive(true);
    }

    public UiMode Mode
    {
      get { return _mode; }
      set
      {
        if (_mode == value)
          return;

        switch (value)
        {
          case UiMode.Default:
            SetActiveScreen(DefaultView);
            break;
          case UiMode.Dead:
            SetActiveScreen(DeadView);
            break;
          default:
            throw new ArgumentOutOfRangeException("value", value, null);
        }

        _mode = value;
      }
    }

    public enum UiMode
    {
      Default,
      Dead
    }
  }
}