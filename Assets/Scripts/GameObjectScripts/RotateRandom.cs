using UnityEngine;
using System.Collections;

public class RotateRandom : MonoBehaviour {

  public float magnitude;
  private Vector3 direction;

  void Start () {
    direction = getRandomDirection(magnitude);
  }
  
  void Update () {
    this.transform.Rotate(direction * Time.deltaTime);
  }

  public void ResetRotation() {
    direction = getRandomDirection(magnitude);
  }

  public void ResetRotation(float magnitude) {
    direction = getRandomDirection(magnitude);
  }

  private Vector3 getRandomDirection(float magnitude) {
    return new Vector3 (
      GetRandomPositiveOrNegative (magnitude),
      GetRandomPositiveOrNegative (magnitude),
      GetRandomPositiveOrNegative (magnitude));
  }
  
  private float GetRandomPositiveOrNegative(float num) {
    return Random.value > 0.5f ? num : -num;
  }

  public static void StartRotatingRandom(GameObject gameObject, float magnitude) {
    gameObject.AddComponent<RotateRandom> ().ResetRotation(magnitude);

  }
  
  public static void StopRotating(GameObject gameObject) {
    Destroy(gameObject.GetComponent<RotateConstant>());
  }
}
