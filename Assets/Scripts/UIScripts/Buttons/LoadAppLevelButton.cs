using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadAppLevelButton : BaseBehaviour
{
  public int level;

  void Start ()
  {
    this.GetComponent<Button> ().onClick.AddListener (() => {
      GameActionsManager.Instance.LoadApplicationLevel(level);
    });
  }
}