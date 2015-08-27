using UnityEngine;
using System.Collections;

public class RotateConstant : BaseBehaviour {

  public Vector3 direction;

	void Update () {
    this.transform.Rotate(direction * Time.deltaTime);
	}

  public static void StartRotating(GameObject gameObject, Vector3 direction) {
    gameObject.AddComponent<RotateConstant>().direction = direction;
  }

  public static void StopRotating(GameObject gameObject) {
    Destroy(gameObject.GetComponent<RotateConstant>());
  }
}