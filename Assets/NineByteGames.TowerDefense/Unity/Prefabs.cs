using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Unity
{
  internal class Prefabs
  {
    /// <summary> The global instance of all prefabs. </summary>
    public static PrefabsBehavior Instance { get; set; }

    /// <summary> Get the prefab by the designated name. </summary>
    /// <param name="nameOfPrefab"> The name of the prefab to retrieve. </param>
    public static Prefab Find(string nameOfPrefab)
    {
      return Instance.FindByName(nameOfPrefab);
    }
  }

  [Serializable]
  public class Prefab
  {
    public string GroupName;

    public GameObject GameObject;

    public GameObject Parent
    {
      get
      {
        if (_parent == null)
        {
          _parent = GameObject.Find(GroupName) ?? new GameObject(GroupName);
        }

        return _parent;
      }
    }

    private GameObject _parent;
  }
}