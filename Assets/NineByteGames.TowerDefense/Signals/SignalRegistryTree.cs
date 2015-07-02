using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;
using Pathfinding;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary>
  ///  Holds a tree that contains nodes that represent the registered listeners for signals that
  ///  were added in a very specific ordered.  It's purpose is to reduce the need to rebuild the
  ///  order of signal listeners every time a new signal listener is added.
  /// </summary>
  /// <remarks>
  ///  When registering listeners, the SignalRegistryTree maintains a list of ordered listeners. The
  ///  very first time a listener is added, the tree adds that node to the root:
  ///  <code>
  ///    * (root)  
  ///      * child: HealthSignal, listeners: Healing (HealthSignal), Death (HealthSignal)
  ///  </code>
  ///  When subsequent signals are added to the child node:
  ///  <code>
  ///    * (root)
  ///      * child: HealthSignal, listeners: Healing (HealthSignal), Death (HealthSignal)  
  ///        *  grandChild: PowerSignal, listeners: Healing (PowerSignal, HealthSignal), Death (HealthSignal)
  ///  </code>
  ///  If another SignalBroadcaster adds signals in the same order (HealthSignal, then PowerSignal)
  ///  then less processing is required as the order of signals has already been determined.
  ///  
  ///  Furthermore, the only allocation that is required is the list that holds the instance
  ///  variable that gets passed to each signal listener.
  /// </remarks>
  public class SignalRegistryTree
  {
    private readonly List<TypeSignalRegistry> _registeredTypeRegisteries;
    private readonly Dictionary<Type, TypeId> _typeLookup = new Dictionary<Type, TypeId>();

    /// <summary> Default constructor. </summary>
    public SignalRegistryTree()
    {
      _registeredTypeRegisteries = new List<TypeSignalRegistry>();
      Root = new SignalNode(this);
    }

    /// <summary> The root node from which all other nodes should be generated. </summary>
    public SignalNode Root { get; private set; }

    /// <summary> Gets the type id for a specific type. </summary>
    /// <param name="type"> The type for which the id should be retrieved. </param>
    /// <returns> The type identifier for the given type. </returns>
    public TypeId GetTypeIdFor(Type type)
    {
      TypeId value;
      if (_typeLookup.TryGetValue(type, out value))
        return value;

      throw new ArgumentException("Type " + type + " does not have an entry", "type");
    }

    /// <summary>
    ///  Creates a new signal registry for the given type, allowing the type to be used when
    ///  registering signals.
    /// </summary>
    /// <param name="type"> The type of the object for which the signal registry should be created </param>
    /// <returns> A TypeSignalRegistry for the given type. </returns>
    public TypeSignalRegistry Create(Type type)
    {
      var typeId = new TypeId(type, _registeredTypeRegisteries.Count);
      var registry = new TypeSignalRegistry(typeId);

      _typeLookup.Add(type, typeId);
      _registeredTypeRegisteries.Add(registry);

      return registry;
    }

    /// <summary> A node that contains the signals listener callbacks that should invoked for each signal type. </summary>
    public class SignalNode
    {
      /// <summary> The tree for which this node is valid. </summary>
      private readonly SignalRegistryTree _tree;

      /// <summary> The lookup of signals that have registered child nodes. </summary>
      private readonly Dictionary<int, SignalNode> _lookup;

      /// <summary> Constructor. </summary>
      /// <param name="tree"> The tree for which this node is valid. </param>
      internal SignalNode(SignalRegistryTree tree)
        : this(tree, new SignalListenerRegistry())
      {
      }

      /// <summary> Constructor. </summary>
      /// <param name="tree"> The tree for which this node is valid. </param>
      /// <param name="registry"> The registry to store with this node. </param>
      private SignalNode(SignalRegistryTree tree, SignalListenerRegistry registry)
      {
        _tree = tree;
        _lookup = new Dictionary<int, SignalNode>();

        Registry = registry;
      }

      /// <summary> The registry associated with the node. </summary>
      public SignalListenerRegistry Registry { get; private set; }

      /// <summary> Gets root node of the tree that this node is attached.. </summary>
      /// <returns> The root node. </returns>
      public SignalNode GetRootNode()
      {
        return _tree.Root;
      }

      /// <summary>
      ///  Retrieves the existing, or creates a new, child node that holds signals for the given type in
      ///  addition to all of the current types for which signals are held.
      /// </summary>
      /// <param name="typeId"> The TypeId of the type for which the node should be retrieved. </param>
      /// <returns>
      ///  A SignalNode that represents this node plus all entries for the type given by TypeId.
      /// </returns>
      [NotNull]
      public SignalNode GetOrCreateChildNodeFor(TypeId typeId)
      {
        SignalNode node;
        if (!_lookup.TryGetValue(typeId.UniqueId, out node))
        {
          var registry = Registry.ClonePlus(_tree._registeredTypeRegisteries[typeId.UniqueId]);
          node = new SignalNode(_tree, registry);

          _lookup.Add(typeId.UniqueId, node);
        }

        return node;
      }
    }
  }
}