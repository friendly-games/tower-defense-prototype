using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Utils;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
  [CustomPropertyDrawer(typeof(TimeField))]
  public class TimeFieldDrawer : PropertyDrawer
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
      SerializedProperty timeValue = property.FindPropertyRelative("_timeValue");

      // get the name of the property
      var niceName = ObjectNames.NicifyVariableName(property.name) + " (in ms)";

      int timeInMs = (int)(timeValue.floatValue);

      timeInMs = EditorGUI.IntField(position, niceName, timeInMs);

      timeValue.floatValue = timeInMs;
    }
  }
}