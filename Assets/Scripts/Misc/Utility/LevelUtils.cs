using UnityEngine;
using System.Collections;
using System.IO;

public static class LevelUtils {

  private static string SAVE_LEVEL_DATA_PATH = "Assets/Resources/LevelData/";
  private static string LOAD_LEVEL_DATA_PATH = "LevelData/";

  public static int GetLevelsInZone(int zone) {
    return Resources.LoadAll (LOAD_LEVEL_DATA_PATH + zone).Length;
  }

  public static string GetPathForLevelSave(int zone, int level) {
    return string.Format (SAVE_LEVEL_DATA_PATH + "{0}/{1}.xml", zone, level);
  }

  public static string GetPathForLevelLoad(int zone, int level) {
    return string.Format (LOAD_LEVEL_DATA_PATH + "{0}/{1}", zone, level);
  }
}
