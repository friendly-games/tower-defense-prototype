using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.Utils;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors
{
  /// <summary> Spawns items at a designed rate. </summary>
  public class Spawner : AttachedBehavior
  {
    private Transform _transform;

    [Tooltip("The game object that should be cloned on each timer tick")]
    public GameObject Template;

    [Tooltip("The parent of game objects created with this spawner")]
    public GameObject Parent;

    [Tooltip("How Often (in seconds) the object should be cloned")]
    public float Period = 1;

    /// <inheritdoc />
    public void Start()
    {
      _transform = transform;

      CreateCoroutine(Trigger());
    }

    /// <summary> Spawns a new entities. </summary>
    private IEnumerator Trigger()
    {
      while (true)
      {
        var cloned = Template.Clone(_transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));

        if (Parent != null)
        {
          cloned.SetParent(Parent);
        }

        var tracker = cloned.GetComponentInChildren<EntityTrackerBehavior>();
        if (tracker != null)
        {
          // TODO
          tracker.Target = GameObject.Find("Player");
        }

        yield return AdvancedCoroutine.Wait(TimeSpan.FromSeconds(Period));
      }
    }
  }
}