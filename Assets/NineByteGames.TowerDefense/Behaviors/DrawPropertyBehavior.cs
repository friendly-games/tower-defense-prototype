using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace NineByteGames.TowerDefense.Behaviors
{
  /// <summary> A draw property behavior. </summary>
  [RequireComponent(typeof(Text))]
  internal class DrawPropertyBehavior : AttachedBehavior
  {
    private Text _displayText;

    public override void Start()
    {
      base.Start();

      _displayText = GetComponent<Text>();
    }

    public void OnGUI()
    {
      var builder = new ReadableText();
      var current = GameObject.Find("Player").GetComponent<PlayerInput>().CurrentObject;

      if (current != null)
      {
        builder.AddProperty("Name", current.name);

        var readables = current.GetComponentsInChildren(typeof(IReadable))
                               .Cast<IReadable>()
                               .ToArray();

        foreach (var readable in readables)
        {
          readable.AddText(builder);
        }

        var newText = builder.ToString();
        var existingText = _displayText.text;

        if (newText != existingText)
        {
          _displayText.text = newText;
        }
      }
      else
      {
        //
      }
    }
  }
}