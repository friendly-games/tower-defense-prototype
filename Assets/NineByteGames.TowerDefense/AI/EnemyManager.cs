using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.ScriptableObjects;
using UnityEngine;

namespace NineByteGames.TowerDefense.AI
{
  /// <summary> Manages the enemies in the world. </summary>
  internal class EnemyManager : InstanceManagerBase
  {
    public EnemyManager(GameObject parent)
      : base(parent)
    {
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
      return Create(enemyPrefab.Template, position, euler);
    }
  }
}