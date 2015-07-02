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

    public int UniqueId
    {
      get { return _uniqueId; }
    }

    public Type Type
    {
      get { return _type; }
    }
  }
}