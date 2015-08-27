using UnityEngine;
using System.Collections;

public class TimeManager : Singleton<TimeManager> {

  public float LevelTime { set; get; }

  void Awake() {
    GameEvent.OnWin += this.StopAllCoroutines;
    GameEvent.OnPlayerCrash += this.StopAllCoroutines;
    GameEvent.OnBeforeGameLevelLoaded += this.StopAllCoroutines;
    GameEvent.OnAfterGameLevelLoaded += () => {
      DoUpdate (() => LevelTime += Time.deltaTime);
      LevelTime = 0;
    };
  }
}