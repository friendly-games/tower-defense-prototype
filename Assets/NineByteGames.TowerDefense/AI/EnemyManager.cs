using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Behaviors;
using NineByteGames.TowerDefense.ScriptableObjects;
using UnityEngine;

namespace NineByteGames.TowerDefense.AI
{
  internal class EnemyManager : IInstanceManager
  {
    /// <summary> The parent to assign to newly created instances. </summary>
    private readonly GameObject _parent;

    /// <summary> The enemies that currently exist in the world.. </summary>
    private readonly HashSet<GameObject> _enemies;

    public EnemyManager(GameObject parent)
    {
      _parent = parent;
      _enemies = new HashSet<GameObject>();
    }

    public bool CanCreate(EnemyPrefab prefab)
    {
      return true;
    }

    /// <summary> Creates a new instance of the given prefab. </summary>
    /// <param name="enemyPrefab"> The prefab to create an instance of. </param>
    /// <param name="position"> The location at which the new instance should be placed. </param>
    /// <param name="euler"> The rotation at which the new instance should be placed. </param>
    /// <returns> A GameObject representing the newly created instance. </returns>
    public GameObject Create(EnemyPrefab enemyPrefab, Vector3 position, Quaternion euler)
    {
      var cloned = enemyPrefab.Template.Clone(position, euler);
      cloned.SetParent(_parent);

      var prefabBased = cloned.AddComponent<PrefabBasedObjectBehavior>();
      prefabBased.Manager = this;

      return cloned;
    }

    public void NotifyAlive(GameObject instance)
    {
      _enemies.Add(instance);
    }

    public void NotifyDestroy(GameObject instance)
    {
      _enemies.Remove(instance);
    }
  }
}