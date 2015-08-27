using UnityEngine;
using System.Collections;

public class Shakeable : BaseBehaviour {

  private const float SHAKE_VALUE = 0.5f;
  private const float SHAKE_TIME = 0.5f;

  // Earthquake In a Given Direction!!
  public Coroutine Shake(Vector2 direction=default(Vector2), float maxShakeValue=SHAKE_VALUE, float shakeTime=SHAKE_TIME) {
    if (maxShakeValue < SHAKE_VALUE) {
      maxShakeValue = SHAKE_VALUE;
    }

    Vector3 origin = this.transform.position;
    direction = direction.normalized;
    return Interpolate (shakeTime, (t) => {
      float timeFactor = 1 - t;
      Vector3 quakeAmt = Random.Range(-maxShakeValue, maxShakeValue) * timeFactor * direction;
      this.transform.position += quakeAmt;
      this.transform.position = Vector3.Lerp(this.transform.position, origin, t);
    });
  }
}
