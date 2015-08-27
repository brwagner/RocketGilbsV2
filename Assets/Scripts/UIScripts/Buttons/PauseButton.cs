using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class PauseButton : BaseBehaviour {
  void Start () {
    this.GetComponent<Button> ().onClick.AddListener (() => UIEvent.Pause());
  }
}