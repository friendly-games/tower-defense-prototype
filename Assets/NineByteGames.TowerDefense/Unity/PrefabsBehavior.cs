using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace NineByteGames.TowerDefense.Unity
{
  internal class PrefabsBehavior : MonoBehaviour
  {
    [Tooltip("All of the prefabs that can be instantiated")]
    public Prefab[] Prefabs;

    private Dictionary<string, Prefab> _lookupTable;

    public void Start()
    {
      _lookupTable = Prefabs.Where(p => p.GameObject != null)
                            .ToDictionary(p => p.GameObject.name);

      Unity.Prefabs.Instance = this;
    }

    /// <summary> Get the prefab by the designated name. </summary>
    /// <param name="nameOfPrefab"> The name of the prefab to retrieve. </param>
    public Prefab FindByName(string nameOfPrefab)
    {
      return _lookupTable[nameOfPrefab];
    }
  }
}