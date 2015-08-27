using UnityEngine;
using System.Collections;

public class PlayerDeath : BaseBehaviour {

  public GameObject deathPrefab;

  private const float DEATH_SHAKE_FACTOR = 0.1f;

	void Awake () {
    GameEvent.OnPlayerCrash += Die;
	}

  void OnDestroy() {
    GameEvent.OnPlayerCrash -= Die;
  }

  private void Die() {
    this.GetComponent<PlayerPathProjection> ().StopPathProjection ();
    
    GameObject death = Instantiate (deathPrefab) as GameObject;
    death.transform.position = this.transform.position;
    
    float shakiness = DEATH_SHAKE_FACTOR * this.GetComponent<Rigidbody> ().velocity.sqrMagnitude;
    Vector2 direction = this.GetComponent<Rigidbody> ().velocity;
    
    Camera.main.GetComponent<Shakeable> ().Shake (direction, shakiness);
    
    this.GetComponent<PlayerDisplay> ().HidePlayer ();


  }
}