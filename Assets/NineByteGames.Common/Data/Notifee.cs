using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.Common.Data
{
  /// <summary> An object that can be notified of changes. </summary>
  public struct Notifee
  {
    /// <summary> The object to be notified. </summary>
    private readonly INotifee _notfiee;

    /// <summary> The identifier that the notifee can use to differentiate between different notifiers. </summary>
    private readonly int _id;

    /// <summary> Constructor. </summary>
    /// <param name="notfiee"> The object to be notified. </param>
    /// <param name="id"> The identifier that the notifee can use to differentiate between different
    ///  notifiers. </param>
    public Notifee(INotifee notfiee, int id)
    {
      _notfiee = notfiee;
      _id = id;
    }

    /// <summary> Notifies the notifee that a change has occurred. </summary>
    public void Notify()
    {
      if (_notfiee != null)
      {
        _notfiee.NotifyChange(_id);
      }
    }
  }
}