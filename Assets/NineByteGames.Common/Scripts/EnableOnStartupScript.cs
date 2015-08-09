using System.Collections.Generic;
using UnityEngine;

namespace NineByteGames.Common.Scripts
{
  /// <summary> Enables a series of objects on startup. </summary>
  public class EnableOnStartupScript : MonoBehaviour
  {
    [Tooltip("Items to enable on startup")]
    public GameObject[] m_Objects;

    // Use this for initialization
    public void Start ()
    {
      foreach (var go in m_Objects)
      {
        go.SetActive(true);
      }
    }
  }
}
