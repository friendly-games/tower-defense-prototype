using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.ScriptableObjects
{
  /// <summary> A prefab for an enemy. </summary>
  public class EnemyPrefab : ScriptableObject
  {
    [Tooltip("The prefab from which new instances should be created")]
    public GameObject Template;

    [Tooltip("The maximum number of instances that are allowed in the world at once")]
    public int MaximumNumber;

    [Tooltip("The cost of the enemy in the world")]
    public int Cost;
  }
}