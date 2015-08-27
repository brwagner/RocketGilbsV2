using UnityEngine;
using System.Collections;
using System;

public class IEnumeratorUtils  {
  public static IEnumerator WaitUntil(Func<bool> pred) {
    while (!pred()) {
      yield return null;
    }
  }
  
  public static IEnumerator Interpolate(float time, Action<float> callback) {
    float startTime = Time.time;
    while (Time.time-startTime < time) {
      callback((Time.time - startTime)/time);
      yield return null;
    }
    callback (1);
  }
  
  public static IEnumerator DoForTime(float time, Action callback) {
    float startTime = Time.time;
    while (Time.time-startTime < time) {
      callback ();
      yield return null;
    }
  }

  public static IEnumerator DoUpdate(Action callback) {
    while (true) {
      callback();
      yield return null;
    }
  }
}