//using System;
//using System.Collections.Generic;
//using System.Linq;
//using NineByteGames.TowerDefense.Messages;

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
//    void Register<TData>(SignalType<TData> any, SingleCallbackNoData callback);

//    void Register<TData>(SignalType<TData> any, SingleCallback<TData> callback);

//    void Register<TData>(SignalType<TData> any, SingleCallbackWithOptions<TData> callback);
//  }

//  public delegate void SingleCallbackNoData();

//  public delegate void SingleCallback<TData>(TData data);

//  public delegate void SingleCallbackWithOptions<TData>(TData data, ref SignalCallbackOptions options);

//  public struct SignalCallbackOptions
//  {
//    public bool ShouldContinue;
//  }

//  public class AllSignals
//  {
//    public static SignalType<Damage> Damage = new SignalType<Damage>();
//    public static SignalType<Healing> Health = new SignalType<Healing>();
//  }

//  public class SignalType<TDataType>
//  {
//  }

//  public class TestClass
//  {
//    void RegisterListeners(ISignalRegistration register)
//    {
//      register.Register(AllSignals.Damage, HandleDamage);
//      register.Register(AllSignals.Health, HandleHealth);
//    }

//    private void HandleDamage(Damage damage, ref SignalCallbackOptions options)
//    {
//      Health -= damage.DamageAmount;

//      if (Health <= 0)
//      {
//        Broadcaster.Send(SignalIndicators.Death);
//        options.ShouldContinue = false;
//      }
//    }

//    private void HandleHealth(Healing health)
//    {
//      float amountToHeal = Math.Min(health.Remaining, MaxHealth - Health);
//      health.Take(amountToHeal);

//      Health += amountToHeal;
//    }
//  }
//}