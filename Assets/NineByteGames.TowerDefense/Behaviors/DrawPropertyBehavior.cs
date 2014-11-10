using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;

namespace NineByteGames.TowerDefense.Behaviors
{
  [RequireComponent(typeof(GUIText))]
  internal class DrawPropertyBehavior : AttachedBehavior
  {
    private GUIText _guiText;

    public override void Start()
    {
      base.Start();

      _guiText = GetComponent<GUIText>();
    }

    public void OnGUI()
    {
      var builder = new ReadableText();
      var current = GameObject.Find("Player").GetComponent<PlayerInput>().CurrentObject;

      if (current == null)
      {
        //_guiText.text = "";
      }
      else
      {
        builder.AddProperty("Name", current.name);

        var readables = current.GetComponentsInChildren(typeof(IReadable)).Cast<IReadable>().ToArray();

        foreach (var readable in readables)
        {
          readable.AddText(builder);
        }

        _guiText.text = builder.ToString();
      }
    }
  }
}