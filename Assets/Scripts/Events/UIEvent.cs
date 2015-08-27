using UnityEngine;
using System.Collections;
using System;

public static class UIEvent {

  public static Action OnThrust;
  public static void Thrust() {
    HandleAction (OnThrust);
  }

  public static Action OnBrake;
  public static void Brake() {
    HandleAction (OnBrake);
  }

  public static Action OnStop;
  public static void Stop() {
    HandleAction (OnStop);
  }

  public static Action OnReset;
  public static void Reset() {
    HandleAction (OnReset);
  }

  public static Action OnPause;
  public static void Pause() {
    HandleAction (OnPause);
  }

  private static void HandleAction(Action action) {
    if (action != null) {
      action();
    }
  }
}