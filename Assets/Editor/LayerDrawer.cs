using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NineByteGames.TowerDefense;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
  [CustomPropertyDrawer(typeof(Layer))]
  public class LayerDrawer : PropertyDrawer
  {
    /// <inheritdoc />
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      // if it's different from the prefab, go ahead and bold it
      if (property.isInstantiatedPrefab)
      {
        SetBoldDefaultFont(property.prefabOverride);
      }

      // get the current value of the field
      SerializedProperty layerNumberProperty = property.FindPropertyRelative("LayerBit");

      // find out which one should be selected by default
      var allLayers = Layer.GetAllLayers();
      var layerNames = allLayers.Select(l => l.Name).ToArray();

      var layerIndex = Array.FindIndex(allLayers, l => l.LayerId == layerNumberProperty.intValue);

      // use 0 if the current layer was not found
      if (layerIndex == -1)
      {
        layerIndex = 0;
      }

      // get the name of the property
      var niceName = ObjectNames.NicifyVariableName(property.name);

      layerIndex = EditorGUI.Popup(position, niceName, layerIndex, layerNames);

      // store the id of the layer that was selected
      layerNumberProperty.intValue = allLayers[layerIndex].LayerId;
    }

    // http://answers.unity3d.com/questions/62455/how-do-i-make-fields-in-the-inspector-go-bold-when.html?sort=oldest
    private static MethodInfo _boldFontMethodInfo = null;

    private static void SetBoldDefaultFont(bool value)
    {
      if (_boldFontMethodInfo == null)
        _boldFontMethodInfo = typeof(EditorGUIUtility).GetMethod("SetBoldDefaultFont",
                                                                 BindingFlags.Static | BindingFlags.NonPublic);
      _boldFontMethodInfo.Invoke(null, new[] {value as object});
    }
  }
}