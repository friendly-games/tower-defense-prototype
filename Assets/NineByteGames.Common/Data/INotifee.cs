using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.Common.Data
{
  /// <summary> An object that can be notified that a change has occurred. </summary>
  public interface INotifee
  {
    /// <summary> Notifies the instance that change has occurred. </summary>
    /// <param name="id"> The identifier that the notifee can use to differentiate between different
    ///  notifiers. </param>
    void NotifyChange(int id);
  }
}