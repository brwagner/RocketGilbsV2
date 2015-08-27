using UnityEngine;
using System.Collections;
using System;

public class PlayerWin : BaseBehaviour {

	void Awake () {
    GameEvent.OnWin += VictoryDance;
	}

  void OnDestroy() {
    GameEvent.OnWin -= VictoryDance;
  }

  public void VictoryDance() {
    StartCoroutine (DoVictoryDance());
    GameEvent.OnWin -= VictoryDance;
  }

  public IEnumerator DoVictoryDance() {
    Game.Player.GetComponent<PlayerPathProjection> ().StopPathProjection();
    Game.Player.GetComponent<PlayerMovement> ().FreezePlayer ();
    RotateConstant.StartRotating(Game.Player, Vector3.left * 100);
    yield return new WaitForSeconds (1);
    LevelManager.Instance.LoadNextGameLevel ();
  }
}
