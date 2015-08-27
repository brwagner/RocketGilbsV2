using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

public class LevelData {
  public List<ItemData> items = new List<ItemData>();

  public void SaveToFile(int zone, int level) {

    string levelFilePath = LevelUtils.GetPathForLevelSave (zone, level);

    if (!Directory.Exists (Path.GetDirectoryName(levelFilePath))) {
      Directory.CreateDirectory(Path.GetDirectoryName(levelFilePath));
    }

    XmlSerializer serializer = new XmlSerializer (typeof(LevelData));
    FileStream stream = new FileStream (levelFilePath, FileMode.Create);
    serializer.Serialize (stream, this);
    stream.Close ();
  }
  
  public static LevelData LoadFromFile(int zone, int level) {

    string levelFilePath = LevelUtils.GetPathForLevelLoad (zone, level);

    TextAsset textAsset = (TextAsset)Resources.Load(levelFilePath, typeof(TextAsset));
    XmlSerializer serializer = new XmlSerializer (typeof(LevelData));
    StringReader stream = new StringReader (textAsset.text);
    LevelData data = serializer.Deserialize (stream) as LevelData;
    stream.Close ();
    return data;
  }
}