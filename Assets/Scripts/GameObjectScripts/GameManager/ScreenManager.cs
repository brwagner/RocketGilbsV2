using UnityEngine;
using System.Collections;

// Forces camera to 16:9 aspect ratio
public class ScreenManager : Singleton<ScreenManager>
{

  public Rect InitialArea { get; private set; } // The initial play area

  // Use this for initialization
  void Awake ()
  {
    // set the desired aspect ratio
    float targetaspect = 16.0f / 9.0f;
    // determine the game window's current aspect ratio
    float windowaspect = (float)Screen.width / (float)Screen.height;
    // current viewport height should be scaled by this amount
    float scaleheight = windowaspect / targetaspect;

    ScaleViewPort (scaleheight);
    InitialArea = GetInitialArea (targetaspect);
  }

  // Sets up the letter or pillar box
  private void ScaleViewPort (float scaleheight)
  {
    // if scaled height is less than current height, add letterbox
    if (scaleheight < 1.0f) {
      Rect rect = Camera.main.rect;
      rect.width = 1.0f;
      rect.height = scaleheight;
      rect.x = 0;
      rect.y = (1.0f - scaleheight) / 2.0f;
      Camera.main.rect = rect;
    }
    else {
      // add pillarbox
      float scalewidth = 1.0f / scaleheight;
      Rect rect = Camera.main.rect;
      rect.width = scalewidth;
      rect.height = 1.0f;
      rect.x = (1.0f - scalewidth) / 2.0f;
      rect.y = 0;
      Camera.main.rect = rect;
    }
  }

  // initializes the play area used to calculate the bounds of the level
  private Rect GetInitialArea (float targetaspect)
  {
    float cameraHeight = Camera.main.orthographicSize * 2;
    float cameraWidth = cameraHeight * targetaspect;
    Vector3 cPos = Camera.main.transform.position;
    return new Rect (cPos.x - cameraWidth / 2, cPos.y - cameraHeight / 2, cameraWidth, cameraHeight);
  }
}
