using UnityEngine;
using System.Collections;

// Holds tasks that manage camera movement
public class CameraTransitions : BaseBehaviour {

  private const float PAN_TIME = 1f;

  private const float ZOOM_TIME = 1f;
  private const float ZOOM_DISTANCE = 1f;

  // Move camera so the target is in the center of the camera
  public Coroutine PanTo(Vector2 target, float time=PAN_TIME) {
    Vector3 from = this.transform.position;
    Vector3 to = new Vector3 (target.x, target.y, this.transform.position.z);
    return Interpolate(time, (t) => this.transform.position = Vector3.Lerp(from, to, t));
  }

  // Change the camera's field of view
  public Coroutine ZoomTo(float to=ZOOM_DISTANCE, float time=ZOOM_TIME) {
    float from = this.GetComponent<Camera>().orthographicSize;
    return Interpolate (time, (t) => this.GetComponent<Camera>().orthographicSize = Mathf.Lerp (from, to, t));
  }
}