using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NineByteGames.TowerDefense.Behaviors
{
  /// <summary> A component that can help to display the properties of an object. </summary>
  internal interface IReadable
  {
    /// <summary> Adds one or more properties to the string builder </summary>
    /// <param name="builder"> The builder. </param>
    void AddText(ReadableText builder);
  }

  /// <summary> An object passed to readable for formatting the names of properties/datums. </summary>
  public class ReadableText
  {
    /// <summary> Default constructor. </summary>
    public ReadableText()
    {
      StringBuilder = new StringBuilder();
    }

    /// <summary> The string builder used to output text. </summary>
    public StringBuilder StringBuilder { get; private set; }

    /// <summary> Display a property using the format "{property}: {value} </summary>
    public ReadableText AddProperty<T>(string propertyName, T value)
    {
      StringBuilder.AppendFormat("{0}: {1}\n", propertyName, value);
      return this;
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return StringBuilder.ToString();
    }
  }
}