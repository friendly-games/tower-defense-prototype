using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Behaviors.Data
{
  /// <summary> Represents a shared piece of data that comes from the SignalBroadcaster </summary>
  public sealed class SharedValue<T> : SharedValue
  {
    private T _value;

    /// <summary> The value of the shared data. </summary>
    public T Value
    {
      get { return _value; }
      set
      {
        if (EqualityComparer<T>.Default.Equals(value, _value))
          return;

        _value = value;
        OnValueChanged();
      }
    }
  }

  /// <summary> Non-generic form of Shared{T} </summary> 
  public class SharedValue
  {
    /// <summary> Event that is fired when the Value of the SharedValue changes. </summary>
    public event EventHandler ValueChanged;

    /// <summary> Executes the value changed action. </summary>
    protected void OnValueChanged()
    {
      var valueChanged = ValueChanged;

      if (valueChanged != null)
      {
        valueChanged(this, EventArgs.Empty);
      }
    }
  }
}