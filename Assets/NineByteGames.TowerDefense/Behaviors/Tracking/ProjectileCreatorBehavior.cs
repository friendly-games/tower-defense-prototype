﻿using NineByteGames.TowerDefense.Messages;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors.Tracking
{
  internal class ProjectileCreatorBehavior : SignalReceiverBehavior<ProjectileCreatorBehavior>
  {
    /// <summary> The layer that the projectile will be a part of. </summary>
    public Layer ProjectileLayer;

    /// <summary> How long (in ticks) between each projectile being created. </summary>
    public int ProjectilePeriod = 30;

    /// <summary> The object to create when a target has been acquired. </summary>
    public GameObject ProjectileTemplate;

    static ProjectileCreatorBehavior()
    {
      SignalEntryPoint.For<ProjectileCreatorBehavior>()
                      .Register(AllSignals.TargetChanged, (i, d) => i.HandleTargetChanged(d));
    }

    private AdvancedCoroutine _coroutine;
    private TimeSpan _period;

    public override void Start()
    {
      base.Start();

      _period = MathUtils.FromTicks(ProjectilePeriod);
    }

    private IEnumerator StartFiringProjectiles()
    {
      while (true)
      {
        yield return AdvancedCoroutine.Wait(_period);

        if (ProjectileTemplate != null)
        {
          var projectileBehavior = ProjectileTemplate.GetComponent<ProjectileBehavior>();
          projectileBehavior.CreateAndInitializeFrom(Owner.transform, ProjectileLayer);
        }
      }
    }

    void HandleTargetChanged(TargetAquiredSignal targetAquired)
    {
      var shouldFire = !targetAquired.TargetWasLost;

      if (shouldFire)
      {
        if (!_coroutine.IsValid)
        {
          _coroutine = CreateCoroutine(StartFiringProjectiles());
        }
      }
      else
      {
        if (_coroutine.IsValid)
        {
          _coroutine.Stop();
          _coroutine = AdvancedCoroutine.Empty;
        }
      }
    }
  }
}