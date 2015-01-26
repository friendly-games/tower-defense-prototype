using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;

namespace NineByteGames.TowerDefense.AI
{
  /// <summary> Manages all of the enemies that currently exist in the world. </summary>
  internal class EnemyManagerBehavior : AttachedBehavior
  {
    private HashSet<GameObject> _enemies;

    public void Start()
    {
      _enemies = new HashSet<GameObject>();
    }

    public bool CanCreate(EnemyPrefab prefab)
    {
      return true;
    }

    public GameObject Create(GameObject enemyPrefab)
    {
      var newEmemy = enemyPrefab.Clone();
      return newEmemy;
    }
  }
}