//using NineByteGames.TowerDefense.Messages;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace NineByteGames.TowerDefense.Signals
//{
//  public class SignalRegistration : ISignalRegistration
//  {
//    public void Register<TData>(SignalType<TData> any, SingleCallbackNoData callback)
//    {
//    }

//    public void Register<TData>(SignalType<TData> any, SingleCallback<TData> callback)
//    {
//    }

//    public void Register<TData>(SignalType<TData> any, SingleCallbackWithOptions<TData> callback)
//    {
//    }
//  }

//  public interface ISignalRegistration
//  {
//  }

//  public delegate void SingleCallbackNoData();

//  public delegate void SingleCallback<TData>(TData data);

//  public delegate void SingleCallbackWithOptions<TData>(TData data, SignalCallbackOptions options);

//  public delegate void StaticSingleCallbackWithOptions(SignalCallbackOptions options);

//  public delegate void StaticSingleCallbackWithOptions<TData>(
//    object instance,
//    TData data,
//    SignalCallbackOptions options);

//  public class SignalCallbackOptions
//  {
//    public bool ShouldContinue;
//  }

//  public class AllSignals
//  {
//    public static readonly SignalType<Damage> Damage = new SignalType<Damage>();
//    public static readonly SignalType<Healing> Health = new SignalType<Healing>();
//    public static readonly SignalType Death = new SignalType();
//  }

//  public class SignalType
//  {
//  }

//  public class SignalType<TDataType>
//  {
//  }

//  public class RegistrationOptions
//  {
//    public bool StopProcessing { get; set; }

//    public int Priority { get; set; }
//  }

//  public class Priorities
//  {
//    public const int VeryLow = -1;
//    public const int VeryHigh = -1;
//  }

//  public interface IRegisterableListener
//  {
//    void Register(Context context);
//  }

//  public class Context
//  {
//    private readonly Dictionary<Type, SignalListenerRegistery.Registry> _lookup = new Dictionary<Type, SignalListenerRegistery.Registry>();

//    public Registry<T> For<T>()
//    {
//      var registery = new Registry<T>();
//      _lookup[typeof(T)] = registery;
//      return registery;
//    }
//  }

//  public class Registry<T> : SignalListenerRegistery.Registry
//  {
//    //public Action<T, TData> Register<TData>(SignalType<TData> signalType, Expression<Func<T, Action<TData>>> callback)
//    //{
//    //  var operand = ((UnaryExpression)callback.Body).Operand;
//    //  var args = ((MethodCallExpression)operand).Arguments;
//    //  var obj = args[2];
//    //  var constant = ((ConstantExpression)obj);

//    //  MethodInfo info = (MethodInfo)constant.Value;

//    //  var call = Delegate.CreateDelegate(typeof(Action<T, TData>), info);

//    //  return (Action<T, TData>)call;
//    //}

//    //public Registry<T> Register(SignalType signalType,
//    //                            Expression<Func<T, Action, SignalCallbackOptions>> callback)
//    //{
//    //  return this;
//    //}

//    public Registry<T> Register(SignalType signalType,
//                                Action<T> callback,
//                                RegistrationOptions options = null)
//    {
//      RegisterSimple(signalType, (i, o) => callback((T)i));
//      return this;
//    }

//    public Registry<T> Register(SignalType signalType,
//                                Action<T, SignalCallbackOptions> callback,
//                                RegistrationOptions options = null)
//    {
//      RegisterSimple(signalType, (i, o) => callback((T)i, o));
//      return this;
//    }

//    public Registry<T> Register<TData>(SignalType<TData> signalType,
//                                       Action<T, TData> callback,
//                                       int priority = 0)
//    {
//      RegisterSimple(signalType, (i, d, o) => callback((T)i, d));
//      return this;
//    }

//    public Registry<T> Register<TData>(SignalType<TData> signalType,
//                                       Action<T, TData, SignalCallbackOptions> callback,
//                                       int priority = 0)
//    {
//      RegisterSimple(signalType, (i, d, o) => callback((T)i, d, o));
//      return this;
//    }
//  }

//  public class SignalListenerRegistery
//  {
//    private static readonly Dictionary<Type, Registry> _lookup = new Dictionary<Type, Registry>();

//    public static Registry<T> For<T>()
//    {
//      var registery = new Registry<T>();
//      _lookup[typeof(T)] = registery;
//      return registery;
//    }

//    public delegate void SimpleCallbackDelegate<TInstance>(TInstance instance);

//    public delegate void SimpleCallbackDelegateWithOptions<TInstance>(TInstance instance, SignalCallbackOptions options);

//    public delegate void CallbackDelegate<TInstance, TData>(TInstance instance, TData data);

//    public delegate void CallbackDelegateWithOptions<TInstance, TData>(
//      TInstance instance,
//      TData data,
//      SignalCallbackOptions options);

//    public class Registry
//    {
//      protected void RegisterSimple<TData>(SignalType<TData> signalType,
//                                           Action<object, TData, SignalCallbackOptions> callback)
//      {
//      }

//      protected void RegisterSimple(SignalType signalType,
//                                    Action<object, SignalCallbackOptions> callback)
//      {
//      }
//    }
//  }

//  public class TestClass
//  {
//    //void RegisterListeners(ISignalRegistration register)
//    //{
//    //  register.Register(AllSignals.Damage, HandleDamage);
//    //  register.Register(AllSignals.Health, HandleHealth);
//    //}

//    //private void HandleDamage(Damage damage, ref SignalCallbackOptions options)
//    //{
//    //  Health -= damage.DamageAmount;

//    //  if (Health <= 0)
//    //  {
//    //    Broadcaster.Send(SignalIndicators.Death);
//    //    options.ShouldContinue = false;
//    //  }
//    //}

//    //private void HandleHealth(Healing health)
//    //{
//    //  float amountToHeal = Math.Min(health.Remaining, MaxHealth - Health);
//    //  health.Take(amountToHeal);

//    //  Health += amountToHeal;
//    //}
//  }
//}