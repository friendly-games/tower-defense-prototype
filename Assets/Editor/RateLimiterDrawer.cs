using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Utils;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
  [CustomPropertyDrawer(typeof(RateLimiter))]
  public class RateLimiterDrawer : PropertyDrawer
  {
    /// <inheritdoc />
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      // if it's different from the prefab, go ahead and bold it
      if (property.isInstantiatedPrefab)
      {
        EditorUtils.SetBoldDefaultFont(property.prefabOverride);
      }

      // get the current value of the field
      SerializedProperty rechargeRate = property.FindPropertyRelative("_rechargeRate");

      // get the name of the property
      var niceName = ObjectNames.NicifyVariableName(property.name) + " (in ms)";

      int timeInMs = (int) (rechargeRate.floatValue * 1000);

      timeInMs = EditorGUI.IntField(position, niceName, timeInMs);

      rechargeRate.floatValue = timeInMs / 1000.0f;
    }
  }
}