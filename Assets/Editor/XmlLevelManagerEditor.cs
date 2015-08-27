using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(LevelManager))]
public class XmlLevelManagerEditor : Editor {

  public override void OnInspectorGUI()
  {
    DrawDefaultInspector();

    LevelManager levelMan = target as LevelManager;
    if(GUILayout.Button("Load"))
    {
      levelMan.LoadLevelInEditor();
    }
    if(GUILayout.Button("Save"))
    {
      levelMan.SaveGameLevel();
    }
  }
}