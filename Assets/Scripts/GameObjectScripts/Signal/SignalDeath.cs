using UnityEngine;
using System.Collections;

public class SignalDeath : MonoBehaviour {

  public GameObject deathPrefab;

	public void Die() {
    GameObject death = Instantiate (deathPrefab);
    death.transform.position = this.transform.position;
    Destroy (this.gameObject);
  }
}