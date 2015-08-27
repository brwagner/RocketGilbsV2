using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SceneObjectManager : Singleton<SceneObjectManager> {

  private static Dictionary<Type, UnityEngine.Object[]> objectPool = new Dictionary<Type, UnityEngine.Object[]>();

  void Awake() {
    GameEvent.OnAfterGameLevelLoaded += () => objectPool.Clear();
    GameEvent.OnSceneInitialized += () => objectPool.Clear();
  }

  public static UnityEngine.Object[] FindCachedObjectsOfType<T>() {
    if (objectPool.ContainsKey (typeof(T))) {
      return objectPool[typeof(T)];
    }
    UnityEngine.Object[] result = GameObject.FindObjectsOfType(typeof(T));
    objectPool.Add (typeof(T), result);
    return result;
  }

  public static UnityEngine.Object FindCachedObjectOfType<T>() {
    UnityEngine.Object[] result = FindCachedObjectsOfType<T> ();
    return result != null ? result [0] : null;
  }
}