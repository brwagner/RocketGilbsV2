using UnityEngine;
using System.Collections;
using System;

public abstract class AbstractInitializer : BaseBehaviour {
  public GameObject managersPrefab;

  void Start() {
    if (GameObject.Find(managersPrefab.name) == null) {
      GameObject go = Instantiate(managersPrefab);
      go.name = managersPrefab.name;
    }
    GameEvent.SceneInitialized ();
    PostStart ();
    Destroy (this.gameObject);
  }

  protected abstract void PostStart();
}