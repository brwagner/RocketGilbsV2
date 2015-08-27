using UnityEngine;
using System.Collections;

public class GameActionsManager : Singleton<GameActionsManager> {

  private static float storedTimeScale = 1;

  void Awake() {
    UIEvent.OnPause += PauseGame;
  }

  public void PauseGame() {
    if (Time.timeScale == 0) {
      Time.timeScale = storedTimeScale;
    } else {
      storedTimeScale = Time.timeScale;
      Time.timeScale = 0;
    }
  }

  public void Update() {
    if (Input.GetKeyDown (KeyCode.Escape)) {
      LoadApplicationLevel(0);
    }
  }

  public void LoadApplicationLevel(int level) {
    StartCoroutine(DoLoadApplicationLevel (level));
  }
  
  private IEnumerator DoLoadApplicationLevel(int level) {
    Time.timeScale = 1;
    storedTimeScale = 1;
    yield return FadeManager.Instance.FadeOut ();
    Application.LoadLevel(level);
  }
}