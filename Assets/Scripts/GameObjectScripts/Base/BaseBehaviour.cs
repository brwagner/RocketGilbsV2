using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

 public abstract class BaseBehaviour : MonoBehaviour {

  public Coroutine WaitUntil(Func<bool> pred) {
    return StartCoroutine (IEnumeratorUtils.WaitUntil (pred));
  }

  public Coroutine Interpolate(float time, Action<float> callback) {
    return StartCoroutine (IEnumeratorUtils.Interpolate (time, callback));
  }

  public Coroutine DoForTime(float time, Action callback) {
    return StartCoroutine (IEnumeratorUtils.DoForTime (time, callback));
  }

  public Coroutine DoUpdate(Action callback) {
    return StartCoroutine (IEnumeratorUtils.DoUpdate(callback));
  }
}
