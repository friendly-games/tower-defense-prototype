using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.General;
using NineByteGames.TowerDefense.Player;
using NineByteGames.TowerDefense.World;
using UnityEngine;
using UnityEngine.Serialization;

namespace NineByteGames.TowerDefense.Equipment
{
  /// <summary> Represents an item that is a projectile for a tool or weapon. </summary>
  public class Projectile : ScriptableObject, IInventoryItemBlueprint
  {
    #region Unity Properties

    [Tooltip("The name of the weapon")]
    public string m_DisplayName;

    [Tooltip("The prefab for the projectile in Unity")]
    [FormerlySerializedAs("m_ProjectilePrefab")]
    public GameObject m_Prefab;

    [Tooltip("The number of these projectiles that can be stacked")]
    public int m_StackAmount;

    #endregion

    IInventoryInstance IInventoryItemBlueprint.CreateInstance(IWorld world, IPlayer player)
    {
      return null;
    }

    /// <inheritdoc/>
    string IInventoryItemBlueprint.Name
    {
      get { return m_DisplayName; }
    }

    /// <inheritdoc/>
    int IInventoryItemBlueprint.MaximumStackAmount
    {
      get { return m_StackAmount; }
    }
  }
}