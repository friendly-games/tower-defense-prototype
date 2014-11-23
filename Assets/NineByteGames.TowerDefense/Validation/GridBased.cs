using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Validation
{
  /// <summary> An object that must be snapped to the grid. </summary>
  internal class GridBased : MonoBehaviour, IEditorBehavior
  {
    public OffsetTypeEnum OffsetType;

    public virtual void ValidateInEditor()
    {
      Vector3 offset;

      switch (OffsetType)
      {
        case OffsetTypeEnum.Size64:
          offset = new Vector3();
          break;

        case OffsetTypeEnum.Size128:
          offset = new Vector3(.5f, .5f, .5f);
          break;

        default:
          throw new ArgumentOutOfRangeException();
      }

      var expected = GridCoordinate.FromVector3(transform.position - offset).ToVector3() + offset;

      if (transform.position != expected)
      {
        transform.position = expected;
      }
    }

    public enum OffsetTypeEnum
    {
      Size64,
      Size128,
    }
  }
}