using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class WinManager : Singleton<WinManager> {
 
	void Awake () {
    GameEvent.OnAfterGameLevelLoaded += () => StartCoroutine (HandleWinCondition ());
    GameEvent.OnMenuInitialized += StopAllCoroutines;
	}

  private IEnumerator HandleWinCondition () {
    yield return WaitUntil (() => Game.Signals.Length == 0);
    GameEvent.Win ();
  }
}