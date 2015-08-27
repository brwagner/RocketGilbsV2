using UnityEngine;
using System.Collections;

// Used to fade the scene in or out
public class FadeManager : Singleton<FadeManager> {

  private const float FADE_IN_TIME = 1f; // time of transition
  private const float FADE_OUT_TIME = 0.5f; // time of transition

  private const int DRAW_DEPTH = -1000; // sets the texture in front of GUI

  private Texture2D fadeOutTexture; // texture brought in
  private float alpha = 1.0f; // transparency of fade

  // Fade the texture in or out
	void OnGUI () {
    GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
    GUI.depth = DRAW_DEPTH;
    GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);
  }

  void Awake() {
    fadeOutTexture = Texture2DUtils.MakeTexture (Color.black);
    GameEvent.OnSceneInitialized += () => FadeIn();
  }

  // Fade out task
  public Coroutine FadeOut() {
    return Fade (alpha, 1, FADE_OUT_TIME);
  }

  // Fade in task
  public Coroutine FadeIn() {
    return Fade (alpha, 0, FADE_IN_TIME);
  }

  // Fades the scene in or out in the given time
  private Coroutine Fade(float start, float end, float time) {
    StopAllCoroutines ();
    return Interpolate (time, (t) => alpha = Mathf.Lerp(start, end, t));
  }
}
