using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.TowerDefense.Player
{
  /// <summary> Contains a lookup table of attachment points to their location and rotation. </summary>
  public class AttachmentToPositionLookup
  {
    private readonly Dictionary<int, LocationAndRotation> _lookup;

    /// <summary> Constructor. </summary>
    /// <param name="lookup"> The lookup table to use for the positions/rotations. </param>
    public AttachmentToPositionLookup(Dictionary<int, LocationAndRotation> lookup)
    {
      _lookup = lookup;
    }

    /// <summary> Try to lookup the given object. </summary>
    /// <param name="attachmentPoint"> The attachment point. </param>
    /// <returns> A LocationAndRotation? </returns>
    public LocationAndRotation? TryGet(AttachmentPoint attachmentPoint)
    {
      LocationAndRotation value;

      return _lookup.TryGetValue((int)attachmentPoint, out value)
        ? (LocationAndRotation?)value
        : null;
    }

    public LocationAndRotation this[AttachmentPoint point]
    {
      get { return TryGet(point) ?? new LocationAndRotation(); }
    }
  }
}