using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Items;

namespace NineByteGames.TowerDefense.UI
{
  /// <summary> Interview for the view that displays the current inventory. </summary>
  internal interface IInventoryDisplayView
  {
    /// <summary> Sets the text for the given slot. </summary>
    /// <param name="index"> Zero-based index of the. </param>
    /// <param name="text"> The text. </param>
    void UpdateSlotText(int index, string text);

    /// <summary> The currently selected slot. </summary>
    int SelectedSlot { get; set; }

    /// <summary> Updates the UI to display the latest amount </summary>
    /// <param name="amount"> The amount to display. </param>
    void UpdateMoney(Money amount);
  }
}