using UnityEngine;
using System.Collections;
using System;

// Manages player contact with other objects
public class PlayerCollisions : BaseBehaviour
{
  // Die when you hit a planet
  void OnCollisionEnter (Collision col)
  {
    if (col.gameObject.HasTag(Tag.Planet)) {
      CrashSelf ();
    }
  }

  // Remove the signal when you hit it
  void OnTriggerEnter (Collider col)
  {
    if (col.gameObject.HasTag(Tag.Signal)) {
      col.GetComponent<SignalDeath>().Die();
      this.GetComponent<PlayerPathProjection>().StartPathProjection();
    }
  }

  // Kill the player if they are off screen
  void Update ()
  {
    if (!ScreenManager.Instance.InitialArea.Contains (this.transform.position)) {
      CrashSelf ();
    }
  }

  // Disable the orbit path and create a death explosion prefab
  private void CrashSelf ()
  {
    GameEvent.PlayerCrash ();
  }
}