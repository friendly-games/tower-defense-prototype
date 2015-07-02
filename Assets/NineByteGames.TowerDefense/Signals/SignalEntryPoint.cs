using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> Serves as an entry point for registering data related to signals. </summary>
  public class SignalEntryPoint
  {
    private static readonly SignalRegistryTree Tree = new SignalRegistryTree();

    /// <summary> Creates a TypeBasedRegistry{T} for a specific type. </summary>
    public static TypedBasedRegistry<T> For<T>()
    {
      return new TypedBasedRegistry<T>(Tree.Create(typeof(T)));
    }

    /// <summary> Creates a new SignalBroadcaster for a new instance. </summary>
    public static SignalBroadcaster CreateBroadcaster()
    {
      // TODO could we pool this?
      return new SignalBroadcaster(Tree);
    }

    /// <summary> Gets a type ID for the given type. </summary>
    public static TypeId GetIdFor(Type type)
    {
      return Tree.GetTypeIdFor(type);
    }
  }
}