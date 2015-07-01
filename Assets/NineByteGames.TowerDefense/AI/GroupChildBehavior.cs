using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.TowerDefense.Behaviors.Tracking;
using NineByteGames.TowerDefense.Signals;
using NineByteGames.TowerDefense.Unity;

namespace NineByteGames.TowerDefense.AI
{
  /// <summary>
  ///  A child that is part of a larger group that acts together towards a common goal.
  /// </summary>
  public class GroupChildBehavior : ChildBehavior
  {
    private IGroupGuider _parent;

    public void Initialize(IGroupGuider parent)
    {
      _parent = parent;
      _parent.Attach(this.gameObject);
      _parent.CurrentTargetChanged += HandleTargetChanged;

      HandleTargetChanged(null, null);
    }

    private void HandleTargetChanged(object sender, EventArgs e)
    {
      var moveTowards = GetComponent<MoveTowardsTargetBehavior>();
      var rotateTowards = GetComponent<RotateTowardsTargetBehavior>();

      if (_parent.CurrentTarget == null)
      {
        moveTowards.enabled = false;
        rotateTowards.enabled = false;
      }
      else
      {
        moveTowards.enabled = true;
        rotateTowards.enabled = true;

        moveTowards.Target = _parent.CurrentTarget;
        rotateTowards.Target = _parent.CurrentTarget;
      }
    }

    [UnityMethod]
    public override void OnDestroy()
    {
      base.OnDestroy();
      _parent.Remove(this.gameObject);
    }
  }
}