using UnityEngine;
using System.Collections;

public static class TransformExtensions {
  public static bool HasTag(this Transform transform, params Tag[] tags) {
    return transform.gameObject.HasTag (tags);
  }
}