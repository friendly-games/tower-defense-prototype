using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> A single id associated with a type. </summary>
  public struct TypeId
  {
    private readonly int _uniqueId;
    private readonly Type _type;

    /// <summary> Constructor. </summary>
    /// <param name="type"> The type for which the id is valid. </param>
    /// <param name="id"> The identifier of the type. </param>
    internal TypeId(Type type, int id)
    {
      _type = type;
      _uniqueId = id;
    }

    /// <summary> Gets a value indicating whether this object is valid. </summary>
    public bool IsValid
    {
      get { return Type != null; }
    }

    /// <summary> Gets the globally unique id for the type. </summary>
    public int UniqueId
    {
      get { return _uniqueId; }
    }

    /// <summary> Gets the type associated with the TypeId. </summary>
    public Type Type
    {
      get { return _type; }
    }
  }
}