using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (Text))]
public class TimeDisplayInit : BaseBehaviour {	
	void Update () {
    this.GetComponent<Text>().text = ((int)TimeManager.Instance.LevelTime).ToString();
	}
}