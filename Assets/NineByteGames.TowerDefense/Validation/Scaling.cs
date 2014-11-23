using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Validation
{
  public class Scaling : MonoBehaviour, IEditorBehavior
  {
    [SerializeField]
    private float _scaling = 1.0f;

    public float ScalingFactor
    {
      get { return _scaling; }
      set
      {
        var newValue = new Vector3(_scaling, _scaling, _scaling);
        if (newValue != transform.localScale)
        {
          transform.localScale = newValue;
        }
      }
    }

    public void ValidateInEditor()
    {
      ScalingFactor = _scaling;
    }
  }
}