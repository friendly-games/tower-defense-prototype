using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace NineByteGames.TowerDefense.UI
{
  /// <summary> Contains the view to display the inventory. </summary>
  internal class InventoryDisplayBehavior : AttachedBehavior, IInventoryDisplayView
  {
    #region Unity Properties

    [Tooltip("The slots which contain the text components for each inventory item")]
    public GameObject[] TextSlots;

    [Tooltip("The item that provides the background color for the currently selected item")]
    public GameObject SelectedBack;

    /// <summary> The selected item. </summary>

    #endregion
    private int _selectedSlot;

    /// <inheritdoc />
    public void UpdateSlotText(int index, string text)
    {
      TextSlots[index].GetComponent<Text>().text = text;
    }

    /// <inheritdoc />
    public int SelectedSlot
    {
      get { return _selectedSlot; }
      set
      {
        if (_selectedSlot == value)
          return;

        var textSlot = TextSlots[value];
        var rectTransform = SelectedBack.GetComponent<RectTransform>();
        rectTransform.localPosition = textSlot.GetComponent<RectTransform>().localPosition;

        _selectedSlot = value;
      }
    }
  }
}