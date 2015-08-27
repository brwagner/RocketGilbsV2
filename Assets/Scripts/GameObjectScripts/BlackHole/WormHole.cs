using UnityEngine;
using System.Collections;

public class WormHole : BaseBehaviour {
	
  public WormHole exit;

  private const float PLAYER_EXIT_SPEED = 6f;

  void OnTriggerEnter (Collider col) {
    if (col.gameObject == Game.Player) {
      StartCoroutine(Warp(col.gameObject));
    }
  }

  private IEnumerator Warp(GameObject player) {
    SetCoolDown (true);
 
    Vector2 playerVelocity = player.GetComponent<Rigidbody> ().velocity;

    player.GetComponent<PlayerDisplay>().HidePlayer ();

    yield return StartCoroutine (DoWormHoleAnimation(playerVelocity));

    player.GetComponent<PlayerDisplay>().UnHidePlayer ();

    player.transform.position = exit.transform.position;
    player.GetComponent<Rigidbody> ().velocity = playerVelocity.normalized * PLAYER_EXIT_SPEED;

    yield return new WaitForSeconds (0.75f);

    SetCoolDown (false);
  }

  private IEnumerator DoWormHoleAnimation(Vector2 playerVelocity) {
    MusicManager.Instance.ShiftPitchDown (0.1f);
    yield return this.GetComponent<Shakeable>().Shake(playerVelocity);
    yield return exit.GetComponent<Shakeable>().Shake(playerVelocity);
    MusicManager.Instance.ShiftPitchToNormal (0.1f);
  }

  public void SetCoolDown(bool isCool) {
    exit.GetComponent<Collider>().enabled = !isCool;
    exit.GetComponent<Gravity>().enabled = !isCool;
    this.GetComponent<Collider> ().enabled = !isCool;
  }
}