using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace NineByteGames.TowerDefense.Behaviors.UI
{
  /// <summary> Draw all of the information that the player contains. </summary>
  public class DrawPlayerInfoBehavior : AttachedBehavior
  {
    public GameObject Player;

    private Text _displayText;

    public void Start()
    {
      _displayText = GetComponent<Text>();
      StartCoroutine(DrawTextLoop());
    }

    protected virtual GameObject GetObjectToDraw()
    {
      return Player;
    }

    private IEnumerator DrawTextLoop()
    {
      while (true)
      {
        DrawText();

        yield return new WaitForSeconds(.5f);
      }
    }

    private void DrawText()
    {
      var builder = new ReadableText();
      var current = GetObjectToDraw();

      if (current == null)
        return;

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
  }
}