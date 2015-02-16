using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.Common.Structures
{
  /// <summary> Represents a concrete size. </summary>
  public struct Size : IEquatable<Size>
  {
    /// <summary> The width portion of the size. </summary>
    public readonly int Width;

    /// <summary> The height portion of the size. </summary>
    public readonly int Height;

    /// <summary> Constructor. </summary>
    /// <param name="width"> The width portion of the size. </param>
    /// <param name="height"> The height portion of the size. </param>
    public Size(int width, int height)
    {
      Width = width;
      Height = height;
    }

    public bool Equals(Size other)
    {
      return Width == other.Width && Height == other.Height;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      return obj is Size && Equals((Size) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return (Width * 397) ^ Height;
      }
    }

    public static bool operator ==(Size left, Size right)
    {
      return left.Equals(right);
    }

    public static bool operator !=(Size left, Size right)
    {
      return !left.Equals(right);
    }
  }
}