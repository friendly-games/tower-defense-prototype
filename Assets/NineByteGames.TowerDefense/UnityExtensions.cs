using JetBrains.Annotations;
using NineByteGames.TowerDefense.Player;
using NineByteGames.TowerDefense.Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NineByteGames.TowerDefense
{
  public static class UnityExtensions
  {
    /// <summary> Check if the given object is alive. </summary>
    /// <param name="instance"> The object to check. </param>
    /// <returns> true if the object is alive, false otherwise. </returns>
    public static bool IsAlive(this Object instance)
    {
      return instance != null;
    }

    /// <summary> Check if the given object is no longer alive. </summary>
    /// <param name="instance"> The object to check. </param>
    /// <returns> true if the object is not alive, false otherwise. </returns>
    public static bool IsDead(this Object instance)
    {
      return instance == null;
    }

    /// <summary> Destroy the game object associated with he component. </summary>
    /// <param name="component"> The component to act on. </param>
    public static void DestroyOwner(this Component component)
    {
      component.gameObject.Kill();
    }

    /// <summary> Destroy the game object associated with he component. </summary>
    /// <param name="component"> The component to act on. </param>
    public static void RemoveComponent(this Component component)
    {
      Object.Destroy(component);
    }

    /// <summary> Destroy the given game object. </summary>
    /// <param name="gameObject"> The gameObject to act on. </param>
    public static void Kill(this GameObject gameObject)
    {
      //foreach (var componentsInChild in gameObject.GetComponentsInChildren<IDestructor>())
      //{
      //  componentsInChild.OnDestroying();
      //}

      Object.Destroy(gameObject);
    }

    /// <summary> Returns true if the game object is dead or null. </summary>
    public static bool IsDead(this GameObject gameObject)
    {
      return gameObject == null;
    }

    /// <summary> Returns true if a GameObject is reference equal to null. </summary>
    public static bool IsNull(this GameObject gameObject)
    {
      return ReferenceEquals(gameObject, null);
    }

    /// <summary> Check fi two game objects point to the same root. </summary>
    public static bool Is(this GameObject lhs, GameObject rhs)
    {
      if (rhs == null)
        return false;

      var leftSignalSender = lhs.GetSignalSender();
      if (leftSignalSender == EmptySignalBroadcaster.Instance)
        return false;

      return leftSignalSender == rhs.GetSignalSender();
    }

    /// <summary> Create a copy of an object. </summary>
    /// <param name="instance"> The object to clone. </param>
    /// <returns> The new copy of the object. </returns>
    public static T Clone<T>(this T instance)
      where T : Object
    {
      return Object.Instantiate(instance);
    }

    /// <summary> Create a copy of an object. </summary>
    /// <param name="instance"> The object to clone. </param>
    /// <param name="parent"> The object that should be set to the parent of the given object. </param>
    /// <returns> The new copy of the object. </returns>
    public static GameObject Clone(this GameObject instance, GameObject parent)
    {
      var clone = Object.Instantiate(instance);
      clone.SetParent(parent);
      return clone;
    }

    /// <summary> Create a copy of an object. </summary>
    /// <param name="instance"> The object to clone. </param>
    /// <param name="parent"> The object that should be set to the parent of the given object. </param>
    /// <param name="locationAndRotation"> The location and rotation of the clone. </param>
    /// <returns> The new copy of the object. </returns>
    public static GameObject Clone(this GameObject instance,
                                   GameObject parent,
                                   LocationAndRotation locationAndRotation)
    {
      var clone = instance.Clone(parent);

      var transform = clone.transform;
      transform.localPosition = locationAndRotation.Location;
      transform.localRotation = locationAndRotation.Rotation;

      return clone;
    }

    /// <summary> Create a copy of an object. </summary>
    /// <typeparam name="T"> Generic type parameter. </typeparam>
    /// <param name="instance"> The object to clone. </param>
    /// <param name="position"> The position of where the new object should be placed. </param>
    /// <param name="rotation"> The rotation of the new object. </param>
    /// <returns> The new copy of the object. </returns>
    public static T Clone<T>(this T instance, Vector3 position, Quaternion rotation)
      where T : Object
    {
      return (T)Object.Instantiate(instance, position, rotation);
    }

    public static T ReplicateGameObject<T>(this T self)
      where T : MonoBehaviour
    {
      return Object.Instantiate(self.gameObject).GetComponent<T>();
    }

    /// <summary> Get the signal broadcaster for a designated GameObject. </summary>
    /// <param name="gameObject"> The gameObject to act on. </param>
    /// <returns>
    ///  A signal broadcaster that can be used to send messages to objects in the GameObject hierarchy.
    /// </returns>
    [NotNull]
    public static ISignalBroadcaster GetSignalSender(this GameObject gameObject)
    {
      var childBehavior = gameObject.RetrieveInHierarchy<IChildBehavior>();
      if (childBehavior != null)
      {
        return childBehavior.Broadcaster;
      }

      return EmptySignalBroadcaster.Instance;
    }

    /// <summary> Sends a signal to a designated game object. </summary>
    /// <typeparam name="T"> Generic type parameter. </typeparam>
    /// <param name="gameObject"> The gameObject to act on. </param>
    /// <param name="signal"> The signal to send to the game object. </param>
    /// <returns> True if the message was handled, false if it was not. </returns>
    public static bool SendSignal<T>(this GameObject gameObject, T signal)
    {
      var childBehavior = gameObject.RetrieveInHierarchy<IChildBehavior>();

      // doesn't accept signals
      if (childBehavior == null)
        return false;

      var sender = childBehavior.Broadcaster;

      if (sender == null)
      {
        // not initialized yet, so do it in the future
        Debug.Log("Sending message in the future");
        ((MonoBehaviour)childBehavior).StartCoroutine(SendInFuture(childBehavior, signal));
        return false;
      }
      else
      {
        return sender.Send(signal);
      }
    }

    private static IEnumerator SendInFuture<T>(IChildBehavior sender, T signal)
    {
      yield return null;
      sender.Broadcaster.Send(signal);
    }

    /// <summary>
    ///  Get the game object associated with the game object of the parent of this game object.
    /// </summary>
    public static GameObject GetParent(this GameObject child)
    {
      var parent = child.transform.parent;

      return parent == null ? null : child.transform.parent.gameObject;
    }

    /// <summary>
    ///  Get the game object associated with the game object of the parent of this game object.
    /// </summary>
    /// <param name="child"> The child to set the parent of. </param>
    /// <param name="parent"> The parent to set as the child's parent. </param>
    public static void SetParent(this GameObject child, GameObject parent)
    {
      child.transform.parent = parent.transform;
    }

    /// <summary>
    ///  Retrieve the instance of the given interface/object by walking up the chain until it is found.
    /// </summary>
    /// <typeparam name="T"> The interface type to retrieve. </typeparam>
    /// <param name="self"> The game object at which to begin the search. </param>
    /// <returns> An instance of the given interface/object, or null if it could not be found. </returns>
    public static T RetrieveInHierarchy<T>(this GameObject self)
      where T : class
    {
      var instance = self.RetrieveOwnObject<T>();

      while (instance == null)
      {
        self = self.GetParent();

        // we've reached the end, how sad
        if (self == null)
          break;

        instance = self.RetrieveOwnObject<T>();
      }

      return instance;
    }

    /// <summary>
    ///  Get the component of the specified type from the game object, without attempting to go
    ///  through the parent component.
    /// </summary>
    public static T RetrieveOwnObject<T>(this GameObject gameObject)
      where T : class
    {
      return gameObject.GetComponent(typeof(T)) as T;
    }
  }
}