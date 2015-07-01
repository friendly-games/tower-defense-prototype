//using NineByteGames.TowerDefense.Messages;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//namespace NineByteGames.TowerDefense.Signals
//{
//  public class AllSignals
//  {
//    public static readonly SignalType<Damage> Damage = new SignalType<Damage>();
//    public static readonly SignalType<Healing> Health = new SignalType<Healing>();
//    public static readonly SignalType<GameObject> TargetChanged = new SignalType<GameObject>();

//    public static readonly SignalType Death = new SignalType();
//  }

//  public delegate void SignalCallback();

//  public delegate void SignalCallback<TData>(TData data);

//  public class SignalType
//  {
//  }

//  public class SignalType<TData>
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
//    void Register(Registry context);
//  }

//  public class Registry
//  {
//    public void Register(SignalType signalType, SignalCallback callback)
//    {
//    }

//    public void Register<TData>(SignalType<TData> signalType, SignalCallback<TData> callback)
//    {
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