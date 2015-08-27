using UnityEngine;
using System.Collections;

public class ChangeColor : BaseBehaviour {

  private static float CHANGE_TIME = 1f;

  public Color[] colors;

  private static int startingColor = 0;

  private int currentColor;

  void Start () {
    currentColor = (startingColor++) % colors.Length;
    StartCoroutine (DoColorShift ());
	}

  private IEnumerator DoColorShift() {
    while (true) {
      int nextColor = (currentColor + 1) % colors.Length;
      yield return Interpolate(CHANGE_TIME, (t) => {
        this.GetComponent<Renderer>().material.color = Color.Lerp(colors[currentColor], colors[nextColor], t);
      });
      currentColor = nextColor;
    }
  }
}
