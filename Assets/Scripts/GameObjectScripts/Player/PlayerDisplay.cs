using UnityEngine;
using System.Collections;

public class PlayerDisplay : MonoBehaviour {
  public void HidePlayer() {
    this.GetComponent<PlayerPathProjection> ().StopPathProjection ();
    this.GetComponent<PlayerMovement> ().FreezePlayer ();
    this.GetComponent<PlayerCollisions> ().enabled = false;
    this.GetComponent<MeshRenderer> ().enabled = false;
    this.GetComponent<Collider> ().enabled = false;
  }
 
  public void UnHidePlayer() {
    this.GetComponent<PlayerPathProjection> ().StartPathProjection ();
    this.GetComponent<PlayerMovement> ().UnFreezePlayer ();
    this.GetComponent<PlayerCollisions> ().enabled = true;
    this.GetComponent<MeshRenderer> ().enabled = true;
    this.GetComponent<Collider> ().enabled = true;
  }
}
