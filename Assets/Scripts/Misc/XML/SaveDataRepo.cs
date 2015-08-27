using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

public static class SaveDataRepo {

  private static string FILE_NAME = "SaveData.xml";

  private static SaveData dataInstance;

  public static SaveData Data {
    get {
      if (dataInstance == null) {
        if (System.IO.File.Exists(getFilePath()) && new FileInfo(getFilePath()).Length > 0) {
          LoadFromFile();
        } else {
          dataInstance = new SaveData();
        }
      }
      return dataInstance;
    }
    set {
      dataInstance = value;
    }
  }

  public static void Commit() {
    XmlSerializer serializer = new XmlSerializer (typeof(SaveData));
    FileStream stream = new FileStream (getFilePath(), FileMode.Create);
    serializer.Serialize (stream, Data);
    stream.Close ();
  }
  
  private static void LoadFromFile() {
    XmlSerializer serializer = new XmlSerializer (typeof(SaveData));
    FileStream stream = new FileStream (getFilePath(), FileMode.OpenOrCreate);
    dataInstance = serializer.Deserialize (stream) as SaveData;
    stream.Close ();
  }

  // Following method is used to retrive the relative path as device platform
  private static string getFilePath(){
    #if UNITY_EDITOR
    return Application.dataPath +"/Resources/"+FILE_NAME;
    #elif UNITY_ANDROID
    return Application.persistentDataPath+FILE_NAME;
    #elif UNITY_IPHONE
    return Application.persistentDataPath+"/"+FILE_NAME;
    #else
    return Application.dataPath +"/"+ FILE_NAME;
    #endif
  }
}

public class SaveData {
  public int maxLevel;
  public int maxZone;
  public int currentLevel;
  public int currentZone;
}