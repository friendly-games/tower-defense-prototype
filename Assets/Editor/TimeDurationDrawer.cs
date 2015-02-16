using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Unity;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
  [CustomPropertyDrawer(typeof(TimeDuration))]
  public class TimeDurationDrawer : PropertyDrawer
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
      SerializedProperty ticks1 = property.FindPropertyRelative("ticks1");
      SerializedProperty ticks2 = property.FindPropertyRelative("ticks2");

      // get the name of the property
      var niceName = ObjectNames.NicifyVariableName(property.name) + " (in ms)";

      // we always edit in ms
      var timeDuration = new TimeDuration(ticks1.intValue, ticks2.intValue);
      int milliseconds = (int) ((TimeSpan) timeDuration).TotalMilliseconds;

      milliseconds = EditorGUI.IntField(position, niceName, milliseconds);

      // convert back into the structure
      int fakeTicks1, fakeTicks2;
      TimeDuration.Convert(TimeSpan.FromMilliseconds(milliseconds), out fakeTicks1, out fakeTicks2);

      // and save the values
      ticks1.intValue = fakeTicks1;
      ticks2.intValue = fakeTicks2;
    }
  }
}