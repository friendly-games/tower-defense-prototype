using NineByteGames.TowerDefense.AI;
using NineByteGames.TowerDefense.Behaviors.Tracking;
using NineByteGames.TowerDefense.ScriptableObjects;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Messages;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors
{
  /// <summary> Spawns items at a designed rate. </summary>
  public class SpawnerBehavior : AttachedBehavior, IGroupGuider
  {
    private Transform _transform;

    [Tooltip("The game object that should be cloned on each timer tick")]
    public EnemyPrefab Template;

    [Tooltip("The parent of game objects created with this spawner")]
    public GameObject Parent;

    [Tooltip("How Often (in seconds) the object should be cloned")]
    public float Period = 1;

    private EnemyManager _instanceManager;

    /// <inheritdoc />
    public void Start()
    {
      _transform = transform;
      _instanceManager = GameObject.Find("Enemies").GetComponent<EnemyManagerBehavior>().InstanceManager;
      CurrentTarget = GameObject.Find("Player");

      CreateCoroutine(Trigger());
    }

    private int count = 0;

    public void Attach(GameObject instance)
    {
      count++;
    }

    public void Remove(GameObject instance)
    {
      count--;
    }

    public GameObject CurrentTarget { get; private set; }

    /// <summary> Spawns a new entities. </summary>
    private IEnumerator Trigger()
    {
      while (true)
      {
        if (count < 10)
        {
          var randomDirection = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360));

          var cloned = _instanceManager.Create(Template, _transform.position, randomDirection);

          cloned.AddComponent<GroupChildBehavior>().Initialize(this);
          cloned.SendSignal(new TargetAquiredSignal(CurrentTarget));
        }

        yield return AdvancedCoroutine.Wait(TimeSpan.FromSeconds(Period));
      }
    }
  }
}