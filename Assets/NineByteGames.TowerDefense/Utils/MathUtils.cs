using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NineByteGames.TowerDefense.Utils
{
  internal class MathUtils
  {
    public const float TicksPerSecond = 30f;

    public const float SecondsPerTick = 1.0f / TicksPerSecond;

    /// <summary> Convert a number of ticks into a timespan. </summary>
    /// <param name="ticks"> The ticks to convert into a timespan. </param>
    /// <returns> A timespan representing the given number of ticks. </returns>
    public static TimeSpan FromTicks(int ticks)
    {
      return TimeSpan.FromSeconds(SecondsPerTick * ticks);
    }

    /// <summary>
    ///  2D rotate one object to face another.  It aligns the top of the object to be looking at the
    ///  other object's center.
    /// </summary>
    /// <param name="objectToRotate"> The object to rotate. </param>
    /// <param name="target"> The target to rotate towards. </param>
    /// <param name="rateToTurn"> The time to use to interpolate the current rotation to the new rotation.. </param>
    public static void RotateTowards(GameObject objectToRotate, GameObject target, float rateToTurn)
    {
      var targetTransform = target.transform;
      var objectTransform = objectToRotate.transform;

      // get the desired vector rotation
      Vector3 vectorToTarget = targetTransform.position - objectTransform.position;

      // convert it into an engle
      float desiredAngle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) - Mathf.PI / 2;

      // then a Quaternion
      Quaternion desiredQuaternion = Quaternion.AngleAxis(desiredAngle * Mathf.Rad2Deg, Vector3.forward);

      // finally rotate it
      objectTransform.rotation = Quaternion.Slerp(objectTransform.rotation, desiredQuaternion, rateToTurn);
    }

    private static void RotateTowards2(GameObject objectToRotate, GameObject target, float rateToTurn)
    {
      var targetTransform = target.transform;
      var objectTransform = objectToRotate.transform;

      // get the desired vector rotation
      Vector3 vectorToTarget = targetTransform.position - objectTransform.position;

      // convert it into an engle
      float desiredAngle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) - Mathf.PI / 2;
      desiredAngle *= Mathf.Rad2Deg;

      float currentAngle = objectTransform.eulerAngles.z;

      // then a Quaternion
      Quaternion desiredQuaternion = Quaternion.AngleAxis(Mathf.LerpAngle(currentAngle, desiredAngle, rateToTurn),
                                                          Vector3.forward);

      // finally rotate it
      objectTransform.rotation = desiredQuaternion;
    }
  }
}