using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using UnityEngine.Events;

public static class GameEvent {

  public static Action OnWin;
  public static void Win() {
    HandleAction (OnWin);
  }

  public static Action OnPlayerCrash;
  public static void PlayerCrash() {
    HandleAction (OnPlayerCrash);
  }

  public static Action OnBeforeGameLevelLoaded;
  public static void BeforeGameLevelLoaded() {
    HandleAction (OnBeforeGameLevelLoaded);
  }

  public static Action OnAfterGameLevelLoaded;
  public static void AfterGameLevelLoaded() {
    HandleAction (OnAfterGameLevelLoaded);
  }

  public static Action OnSceneInitialized;
  public static void SceneInitialized() {
    HandleAction (OnSceneInitialized);
  }

  public static Action OnMenuInitialized;
  public static void MenuInitialized() {
    HandleAction (OnMenuInitialized);
  }

  public static Action OnGameInitialized;
  public static void GameInitialized() {
    HandleAction (OnGameInitialized);
  }
  
  private static void HandleAction(Action action) {
    if (action != null) {
      action();
    }
  }
}