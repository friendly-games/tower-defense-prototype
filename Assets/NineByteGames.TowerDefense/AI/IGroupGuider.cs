using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.AI
{
  /// <summary> Provides guidance on how an enemy should move in the world. </summary>
  public interface IGroupGuider
  {
    /// <summary> Attaches a new object to this group. </summary>
    /// <param name="instance"> The instance to add. </param>
    void Attach(GameObject instance);

    /// <summary> Removes an object from this group </summary>
    /// <param name="instance"> The instance to remove. </param>
    void Remove(GameObject instance);

    GameObject CurrentTarget { get; }

    event EventHandler CurrentTargetChanged;
  }

  /// <summary> Represents a target that an object should follow. </summary>
  public interface IMovingTarget
  {
    /// <summary> The target that should be followed. </summary>
    Transform CurrentTarget { get; }
  }

  ///// <summary> A group of enemies that has a common target. </summary>
  //public class CommonTargeter : IGroupGuider
  //{
  //  private readonly LinkedList<GameObject> _followers;

  //  /// <summary> The target that all child object should follow. </summary>
  //  public Transform CurrentTarget { get; set; }

  //  /// <summary> Default constructor. </summary>
  //  public CommonTargeter()
  //  {
  //    _followers = new LinkedList<GameObject>();
  //  }

  //  /// <inheritdoc />
  //  public void Attach(GameObject instance)
  //  {
  //    _followers.AddLast(instance);
  //  }

  //  /// <inheritdoc />
  //  public void Remove(GameObject instance)
  //  {
  //    _followers.Remove(instance);
  //  }
  //}
}