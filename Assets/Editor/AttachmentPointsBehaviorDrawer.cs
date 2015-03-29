using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace NineByteGames.TowerDefense.Player
{
  [CustomEditor(typeof(AttachmentPointsBehavior))]
  public class AttachmentPointsBehaviorDrawer : Editor
  {
    private static readonly string[] TypeStrings;

    static AttachmentPointsBehaviorDrawer()
    {
      TypeStrings = Enum.GetNames(typeof(AttachmentPoint));
    }

    public override void OnInspectorGUI()
    {
      var target = (AttachmentPointsBehavior)this.target;

      var targetOwner = target.gameObject;
      var subBehaviors = targetOwner.GetComponentsInChildren<AttachmentTypeBehavior>();

      // have each child object listed along with it's corresponding "Type" as a drop down.
      foreach (var sub in subBehaviors)
      {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel(sub.gameObject.name);

        var newValue = (AttachmentPoint)EditorGUILayout.Popup((int)sub.Type, TypeStrings);
        if (newValue != sub.Type)
        {
          sub.Type = newValue;
          EditorUtility.SetDirty(sub.gameObject);
        }

        EditorGUILayout.EndHorizontal();
      }
    }
  }
}