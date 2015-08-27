using UnityEngine;
using System.Collections;
using System;

public class MusicManager : Singleton<MusicManager> {
    private const float FADE_OUT_TIME = 1f;
  private const float FADE_IN_TIME = 1f;

  private const float PITCH_NORMAL = 1f;
  private const float PITCH_MAX = PITCH_NORMAL + 0.02f;
  private const float PITCH_MIN = PITCH_NORMAL - 0.02f;
  private const float SHIFT_TIME = 0.5f;

  public AudioClip[] clips;

  private int currentClip = 0;

  private AudioSource source;

  void Awake() {
    source = this.GetOrAddComponent<AudioSource> ();
    StartCoroutine (DoMusicLoop());
  }

  private IEnumerator DoMusicLoop() {
    while (true) {
      yield return WaitUntil (() => !source.isPlaying);
      yield return FadeOutMusic ();
      source.clip = clips [currentClip++ % clips.Length];
      source.Play ();
      yield return FadeInMusic ();
    }
  }
 
  private void ShiftPitchBackAndForth() {
    StartCoroutine(DoShiftPitchBackAndForth());
  }

  private IEnumerator DoShiftPitchBackAndForth() {
    yield return ShiftPitchDown ();
    yield return ShiftPitchToNormal ();
  }

  public Coroutine FadeOutMusic(float time=FADE_OUT_TIME) {
    return FadeMusic(0f, time);
  }

  public Coroutine FadeInMusic(float time=FADE_IN_TIME) {
    return FadeMusic(1f, time);
  }

  private Coroutine FadeMusic(float toVolume, float time) {
    float fromVolume = source.volume;
    return Interpolate (time, (t) => source.volume = Mathf.Lerp(fromVolume, toVolume, t));
  }

  public Coroutine ShiftPitchUp(float time=SHIFT_TIME) {
    return ShiftPitch (PITCH_MAX, time);
  }

  public Coroutine ShiftPitchDown(float time=SHIFT_TIME) {
    return ShiftPitch (PITCH_MIN, time);
  }

  public Coroutine ShiftPitchToNormal(float time=SHIFT_TIME) {
    return ShiftPitch (PITCH_NORMAL, time);
  }

  private Coroutine ShiftPitch(float toPitch, float time) {
    float fromPitch = source.pitch;
    return Interpolate (time, (t) => source.pitch = Mathf.Lerp(fromPitch, toPitch, t));
  }
}
