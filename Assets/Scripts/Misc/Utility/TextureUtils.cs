using UnityEngine;
using System.Collections;

public static class Texture2DUtils {

	public static Texture2D MakeTexture(Color color) {
    Texture2D tex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
    tex.SetPixel (0, 0, color);
    tex.Apply ();
    return tex;
  }
}
