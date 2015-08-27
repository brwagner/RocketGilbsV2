using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelectInit : MonoBehaviour {

  public GameObject[] planetPrefabs;
  public GameObject buttonPrefab;

  void Start () {

    FadeManager.Instance.FadeIn ();

    for (int zone = 0; zone <= SaveDataRepo.Data.maxZone; zone++) {
      for (int level = 0; level <= (zone < SaveDataRepo.Data.maxZone ? LevelUtils.GetLevelsInZone(zone) - 1 : SaveDataRepo.Data.maxLevel); level++) {
        GameObject planet = MakePlanetForLevel(zone, level, LevelUtils.GetLevelsInZone(zone));
        MakeButtonOnPlanetForLevel(planet, zone, level);
      }
    }
  }

  private GameObject MakePlanetForLevel (int zone, int level, int zoneSize)
  {    
    Rect screenRect = ScreenManager.Instance.InitialArea;
    float division = screenRect.width / (float)zoneSize;

    GameObject planet = Instantiate (planetPrefabs[zone]) as GameObject;
    planet.transform.position = new Vector3 (screenRect.x + screenRect.width * zone + division / 2 + division * level, 0, 0);
    planet.GetComponent<AGravitational> ().enabled = false;
    return planet;
  }

  private void MakeButtonOnPlanetForLevel (GameObject planet, int zone, int level)
  {
    GameObject button = Instantiate (buttonPrefab) as GameObject;
    button.transform.SetParent (this.transform, false);
    button.transform.position = new Vector3 (planet.transform.position.x, planet.transform.position.y, -5);
    button.GetComponentInChildren<Text> ().text = (level + 1).ToString();
    RegisterListenerToLoadLevel(button, zone, level);
  }

  private void RegisterListenerToLoadLevel(GameObject button, int zone, int level) {
    button.GetComponent<Button>().onClick.AddListener(() => {
      SaveDataRepo.Data.currentZone = zone;
      SaveDataRepo.Data.currentLevel = level;
      LevelManager.Instance.zone = zone;
      LevelManager.Instance.level = level;
      SaveDataRepo.Commit();
      GameActionsManager.Instance.LoadApplicationLevel(3);
    });
  }
}