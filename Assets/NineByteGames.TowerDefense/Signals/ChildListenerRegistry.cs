using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Signals
{
  /// <summary> Manages the types of signals that a given type listens to. </summary>
  public class ChildListenerRegistry
  {
    private static readonly Dictionary<Type, RegisteryInfo> Lookup = new Dictionary<Type, RegisteryInfo>();

    /// <summary> Gets the signals that a type is interested in. </summary>
    public static RegisteryInfo GetFor(Type type)
    {
      RegisteryInfo info;
      if (Lookup.TryGetValue(type, out info))
        return info;

      info = CreateInfo(type);
      Lookup[type] = info;

      return info;
    }

    /// <summary>
    ///  Creates a brand-new registry info for a designed type, retrieving the signals that the type
    ///  is interested in receiving.
    /// </summary>
    private static RegisteryInfo CreateInfo(Type type)
    {
      var interfaceTypes = from interf in type.GetInterfaces()
                           where interf.IsGenericType
                           let typeDef = interf.IsGenericType
                           where interf.IsGenericType
                           select interf;

      var all = new List<KeyValuePair<Type, int>>();

      foreach (var interfaceType in interfaceTypes)
      {
        int priority = SignalPriorities.Normal;

        var method = type.GetInterfaceMap(interfaceType).TargetMethods.First();
        var priorityAtt =
          ((SignalPriorityAttribute[])method.GetCustomAttributes(typeof(SignalPriorityAttribute), false))
            .FirstOrDefault();

        if (priorityAtt != null)
        {
          priority = priorityAtt.Priority;
        }

        var dataType = interfaceType.GetGenericArguments().First();

        all.Add(new KeyValuePair<Type, int>(dataType, priority));
      }

      return new RegisteryInfo(type, all);
    }

    /// <summary> Contains all of the signals that a given type is interested in. </summary>
    public class RegisteryInfo
    {
      public RegisteryInfo(Type type, List<KeyValuePair<Type, int>> entries)
      {
        Entries = entries;
        Type = type;
      }

      /// <summary> The type for which the registry was created. </summary>
      public Type Type { get; private set; }

      /// <summary> A list of signal types and associated priorities of the signals that the type is interested in. </summary>
      public List<KeyValuePair<Type, int>> Entries { get; private set; }
    }
  }
}