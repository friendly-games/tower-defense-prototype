using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> A fluent api on top of TypeSignalRegistry. </summary>
  /// <typeparam name="T"> Generic type parameter. </typeparam>
  public class TypedBasedRegistry<T>
  {
    private readonly TypeSignalRegistry _registry;

    /// <summary> Constructor. </summary>
    /// <param name="registry"> The registry for which items should be added.. </param>
    public TypedBasedRegistry(TypeSignalRegistry registry)
    {
      _registry = registry;
    }

    /// <summary> Registers this object. </summary>
    /// <param name="signalType"> The type of signal to register a call back for. </param>
    /// <param name="callback"> The callback to register. </param>
    /// <param name="priority"> The priority if the callback. </param>
    /// <returns>
    ///  The TypedBasedRegistery that the method was invoked on, for fluent chaining.
    /// </returns>
    public TypedBasedRegistry<T> Register(SignalType signalType, Action<T> callback, int priority = AllPriorities.Normal)
    {
      _registry.Register<EmptyArg>(signalType, (obj, data) => callback((T)obj), priority);
      return this;
    }

    /// <summary> Registers this object. </summary>
    /// <exception cref="ArgumentException"> Thrown when one or more arguments have unsupported or
    ///  illegal values. </exception>
    /// <typeparam name="TData"> The type of data that the signal sends. </typeparam>
    /// <param name="signalType"> The type of signal to register a call back for. </param>
    /// <param name="callback"> The callback to register. </param>
    /// <param name="priority"> The priority if the callback. </param>
    /// <returns>
    ///  The TypedBasedRegistery that the method was invoked on, for fluent chaining.
    /// </returns>
    public TypedBasedRegistry<T> Register<TData>(SignalType<TData> signalType, Action<T, TData> callback, int priority = AllPriorities.Normal)
    {
      if (typeof(TData) == typeof(EmptyArg))
        throw new ArgumentException("Cannot use empty args for registration");

      _registry.Register<TData>(signalType, (obj, data) => callback((T)obj, data), priority);
      return this;
    }
  }
}