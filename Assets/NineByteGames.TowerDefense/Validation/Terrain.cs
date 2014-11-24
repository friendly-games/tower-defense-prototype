using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Validation
{
  internal class Terrain : GridBased
  {
    private static GameObject _terrainObject;

    private GameObject TerrainObject
    {
      get
      {
        if (_terrainObject == null)
        {
          _terrainObject = GameObject.Find("Terrain");
        }

        return _terrainObject;
      }
    }

    /// <summary> Validate the parent is set correctly. </summary>
    public override void ValidateInEditor()
    {
      base.ValidateInEditor();

      if (this.gameObject.GetParent() != TerrainObject)
      {
        this.gameObject.SetParent(TerrainObject);
      }
    }
  }
}