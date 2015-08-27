using UnityEngine;
using System.Collections;

public class ScreenShot : BaseBehaviour {

	void Start () {
    StartCoroutine (ScreenCaptureRoutine());
	}
	
	private IEnumerator ScreenCaptureRoutine() {
    yield return new WaitForSeconds (2);
    Application.CaptureScreenshot ("Assets/Resources/LevelPreviews/" + Application.loadedLevelName + ".png");
    yield return new WaitForSeconds (2);
    Application.LoadLevel (Application.loadedLevel + 1);
  }
}
