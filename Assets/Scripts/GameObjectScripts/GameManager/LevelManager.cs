using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Linq;

public class LevelManager : Singleton<LevelManager> {

  public int zone;
  public int level;

  public bool loadThisLevel;

  private static XmlIO xmlIO = new XmlIO();

  void Awake() {
    zone = SaveDataRepo.Data.currentZone;
    level = SaveDataRepo.Data.currentLevel;
    UIEvent.OnReset += LoadCurrentGameLevel;
    GameEvent.OnGameInitialized += LoadCurrentGameLevel;
  }

  public void LoadCurrentGameLevel() {
    Time.timeScale = 1;
    StartCoroutine (DoLoadCurrentGameLevel ());
  }

  private IEnumerator DoLoadCurrentGameLevel() {
    yield return FadeManager.Instance.FadeOut ();
    LoadGameLevelAndHandleEvents ();
    yield return FadeManager.Instance.FadeIn ();
  }

  private void LoadGameLevelAndHandleEvents() {
    GameEvent.BeforeGameLevelLoaded ();
    xmlIO.LoadLevel (zone, level);
    GameEvent.AfterGameLevelLoaded ();
  }

  public void LoadNextGameLevel() {
    if (level < LevelUtils.GetLevelsInZone(zone) - 1) {
      level++;
    } else {
      level = 0;
      zone++;
    }

    if (LevelUtils.GetLevelsInZone (zone) != 0) {
      SyncSaveData ();
      LoadCurrentGameLevel ();
    } else {
      level = 0;
      zone = 0;
      SyncSaveData();
      GameActionsManager.Instance.LoadApplicationLevel(0);
    }
  }
  
  public void LoadPreviousGameLevel() {
    if (level > 0) {
      level--;
    } else {
      level = LevelUtils.GetLevelsInZone(zone - 1) - 1;
      zone--;
    }

    SyncSaveData ();
   
    LoadCurrentGameLevel ();
  }

  public void SaveGameLevel() {
    xmlIO.SaveLevel (zone, level);
  }

  public void LoadLevelInEditor () {
    xmlIO.LoadLevel (zone, level);
  }

  private void SyncSaveData() {
    SaveDataRepo.Data.currentZone = zone;
    SaveDataRepo.Data.currentLevel = level;
    
    if (SaveDataRepo.Data.maxZone < zone) {
      SaveDataRepo.Data.maxZone = zone;
      SaveDataRepo.Data.maxLevel = level;
    }
    
    if (SaveDataRepo.Data.maxLevel < level) {
      SaveDataRepo.Data.maxLevel = level;
    }
    
    SaveDataRepo.Commit ();
  }

}