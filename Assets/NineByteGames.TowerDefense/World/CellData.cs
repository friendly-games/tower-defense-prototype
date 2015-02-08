using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace NineByteGames.TowerDefense.World
{
  /// <summary> The data associated with a designated located in a grid. </summary>
  [StructLayout(LayoutKind.Explicit, Pack = 1)]
  public struct CellData
  {
    /// <summary> The type of the given cell. </summary>
    [FieldOffset(0)]
    public byte RawType;

    /// <summary> The type of terrain underneath at the given location. </summary>
    [FieldOffset(1)]
    public byte RawTerrainType;

    /// <summary> True if the designated location is considered empty. </summary>
    public bool IsEmpty
    {
      get { return RawType == 0; }
    }

    public TerrainType TerrainType
    {
      get { return (TerrainType) RawTerrainType; }
    }

    public CellType Type
    {
      get { return (CellType) RawType; }
    }
  }

  public enum CellType : byte
  {
    Empty,
    Solid
  }

  /// <summary> Represents a type of terrain. </summary>
  public enum TerrainType : byte
  {
    Grass,
    Dirt,
  }
}