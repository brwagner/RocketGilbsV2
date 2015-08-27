using UnityEngine;
using System.Collections;
using System;

public static class GameObjectExtensions {
  public static bool HasTag(this GameObject gameObject, params Tag[] tags) {
    Tag gameObjectTag = (Tag)Enum.Parse(typeof(Tag), gameObject.tag);
    foreach (Tag tag in tags) {
      if (gameObjectTag == tag) {
        return true;
      }
    }
    return false;
  }

  public static GameObject[] FindGameObjectsWithTag(Tag tag) {
    return GameObject.FindGameObjectsWithTag (tag.ToString ());
  }

  public static GameObject FindGameObjectWithTag(Tag tag) {
    GameObject[] result = FindGameObjectsWithTag (tag);
    return result.Length > 0 ? result[0] : null;
  }
}