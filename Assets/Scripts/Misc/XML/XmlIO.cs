#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

public class XmlIO
{

  public void SaveLevel (int zone, int level)
  {
    LevelData levelData = new LevelData ();
    
    foreach (GameObject gameObject in FindSavableRootGameObjects()) {
      levelData.items.Add (CreateItemDataFromGameObject (gameObject));
    }

    levelData.SaveToFile (zone, level);
  }

  private ItemData CreateItemDataFromGameObject (GameObject gameObject)
  {
    ValidateGameObject (gameObject);

    ItemData itemData = new ItemData ();
    itemData.transformData.position = gameObject.transform.position;
    itemData.transformData.rotation = gameObject.transform.eulerAngles;
    itemData.transformData.scale = gameObject.transform.localScale;
    itemData.name = gameObject.name;

    foreach (IPersistable persistable in gameObject.GetComponents<IPersistable>()) {

      SerializableDictionary<string, object> componentConfiguration = new SerializableDictionary<string, object> ();
      foreach (FieldInfo field in persistable.GetType().GetFields()) {
        componentConfiguration.Add (field.Name, field.GetValue (persistable));
      }

      string componentName = persistable.GetType ().FullName;

      itemData.componentData.configurations.Add (componentName, componentConfiguration);
    }

    foreach (Transform child in gameObject.transform) {
      if (child.GetComponents<IPersistable> ().Length > 0) {
        itemData.children.Add (CreateItemDataFromGameObject (child.gameObject));
      }
    }

    return itemData;
  }

  private void ValidateGameObject (GameObject gameObject)
  {
    if (!NameIsUnique (gameObject)) {
      throw new IOException ("Cannot save gameObject " + gameObject.name + ": Name is not unique");
    }
  }

  private bool NameIsUnique (GameObject gameObject)
  {
    if (gameObject.transform.parent == null) {
      return true;
    }
    foreach (Transform xForm in gameObject.transform.parent) {
      if (gameObject.name.Equals (xForm.name) && gameObject != xForm.gameObject) {
        return false;
      }
    }
    return true;
  }

  public void LoadLevel (int zone, int level)
  {
    foreach (GameObject gameObject in FindSavableRootGameObjects()) {
      GameObject.DestroyImmediate (gameObject);
    }

    LevelData levelData = LevelData.LoadFromFile (zone, level);

    foreach (ItemData itemData in levelData.items) {
      LoadItemData (itemData, LoadPrefab (itemData.name));
    }
  }

  private void LoadItemData (ItemData itemData, GameObject gameObject)
  {
    gameObject.transform.position = itemData.transformData.position;
    gameObject.transform.rotation = Quaternion.Euler (itemData.transformData.rotation);
    gameObject.transform.localScale = itemData.transformData.scale;

    LoadPersistablesInGameObjectFromComponentData (gameObject, itemData.componentData);

    foreach (ItemData childData in itemData.children) {
      LoadItemData (childData, gameObject.transform.FindChild (childData.name).gameObject);
    }
  }

  private void LoadPersistablesInGameObjectFromComponentData (GameObject gameObject, ComponentData componentData)
  {
    foreach (IPersistable persistable in gameObject.GetComponents<IPersistable>()) {
      SerializableDictionary<string, object> componentConfiguration = componentData.configurations [persistable.GetType ().FullName];
      foreach (FieldInfo field in persistable.GetType().GetFields()) {
        field.SetValue (persistable, componentConfiguration [field.Name]);
      }
    }
  }
  
  private GameObject LoadPrefab (string name)
  {
    #if UNITY_EDITOR
    GameObject gameObject = PrefabUtility.InstantiatePrefab (Resources.Load ("Prefabs/" + name)) as GameObject;
    #else
    GameObject gameObject = GameObject.Instantiate (Resources.Load ("Prefabs/" + name)) as GameObject;
    #endif
    gameObject.name = gameObject.name.Replace ("(Clone)", "");
    return gameObject;
  }

  private List<GameObject> FindSavableRootGameObjects ()
  {
    HashSet<GameObject> roots = new HashSet<GameObject> ();
    foreach (Transform xForm in GameObject.FindObjectsOfType<Transform>()) {
      if (xForm.root.GetComponent<NonSavable>() == null) {
        roots.Add (xForm.root.gameObject);
      }
    }
    return roots.ToList ();
  }
}