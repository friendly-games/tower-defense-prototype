using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.Common
{
  /// <summary> Manages various resources over the lifetime of an application. </summary>
  internal class ResourcePool
  {
    /// <summary> The default MaxCount for future instance of ResourcePool{T}. </summary>
    public static int DefaultMaxCount = 8;

    /// <summary> Initializes the object from a ResourcePool{T}. </summary>
    /// <typeparam name="T"> The type of <paramref name="instance"/> </typeparam>
    /// <param name="instance"> [out] The instance to initialize. </param>
    public static void Initialize<T>(out T instance)
      where T : new()
    {
      instance = ResourcePool<T>.Retrieve();
    }

    /// <summary> Stores the given instance back into a resource pool. </summary>
    /// <typeparam name="T"> The data type of <paramref name="instance"/> </typeparam>
    /// <param name="instance"> The instance to store into the resource pool. </param>
    public static void Store<T>(T instance) where T : new()
    {
      ResourcePool<T>.Put(instance);
    }
  }

  /// <summary> Manages various resources over the lifetime of an application. </summary>
  internal static class ResourcePool<T>
    where T : new()
  {
    private static readonly Queue<T> UnusedItems = new Queue<T>();

    /// <summary> The maximum number of items that should be kept. </summary>
    public static int MaxCount = ResourcePool.DefaultMaxCount;

    /// <summary> Retrieve an item from the resource pool, creating a new item if required. </summary>
    /// <returns> An instance of T. </returns>
    public static T Retrieve()
    {
      if (UnusedItems.Count > 0)
      {
        return UnusedItems.Dequeue();
      }
      else
      {
        return new T();
      }
    }

    /// <summary> Add an existing item back to the resource pool. </summary>
    /// <param name="instance"> The instance to put into the resource pool. </param>
    public static void Put(T instance)
    {
      if (UnusedItems.Count < MaxCount)
      {
        UnusedItems.Enqueue(instance);
      }
    }
  }
}