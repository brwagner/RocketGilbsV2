using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class BigResetButton : BaseBehaviour {

  public void Enable() {
    this.gameObject.SetActive(true);
  }

  public void Disable() {
    this.gameObject.SetActive(false);
  }

  void Awake() {
    GameEvent.OnPlayerCrash += Enable;
    GameEvent.OnAfterGameLevelLoaded += Disable;
  }

  void OnDestroy() {
    GameEvent.OnPlayerCrash -= Enable;
    GameEvent.OnAfterGameLevelLoaded -= Disable;
  }

  void Start () {
    this.GetComponent<Button> ().onClick.AddListener (() => UIEvent.Reset());
  }
}