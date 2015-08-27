using UnityEngine;
using System.Collections;

public class PanForLevelSelection : BaseBehaviour
{

  public float speed = 0.1f;
  private int currentZone = 0;

  void Start () {
    StartCoroutine (DoHandleInput());
  }

  public IEnumerator DoHandleInput() {
    while (true) {
      if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) {
        if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
          Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
          if (touchDeltaPosition.x > 0 && currentZone < SaveDataRepo.Data.maxZone) {
            yield return DoSwipe(1);
          } else if (touchDeltaPosition.x < 0 && currentZone > 0) {
            yield return DoSwipe(-1);
          }
        }
      } else {
        if (Input.GetKeyDown (KeyCode.RightArrow) && currentZone < SaveDataRepo.Data.maxZone) {
          yield return DoSwipe(1);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && currentZone > 0) {
          yield return DoSwipe(-1);
        }
      }
      yield return null;
    }
  }

  public Coroutine DoSwipe(float direction) {
    currentZone += (int)direction;
    Vector3 from = this.transform.position;
    Vector3 to = new Vector3(from.x + ScreenManager.Instance.InitialArea.width * direction, from.y, from.z);
    return Interpolate(0.25f, (t) => {
      this.transform.position = Vector3.Lerp(from, to, t);
    });
  }
}