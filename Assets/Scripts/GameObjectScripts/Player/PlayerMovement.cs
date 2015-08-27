using UnityEngine;
using System.Collections;

// Manages player movement
[RequireComponent (typeof (Rigidbody))]
public class PlayerMovement : MonoBehaviour, IPersistable
{

  public float startingX;
  public float startingY;

  private Vector2 storedVelocity;

  // Use this for intialization
  void Start () {
    this.GetComponent<Rigidbody> ().velocity = new Vector2(startingX, startingY);
  }

  // Rigidbody Operations
  void FixedUpdate () {
    this.GetComponent<Rigidbody>().AddForce (this.transform.forward * GameInputManager.Instance.Acceleration * 2); // Accelerate or decelerate based on input
    this.transform.rotation = Quaternion.LookRotation(this.GetComponent<Rigidbody>().velocity);
  }

  // Make the player unable to move
  public void FreezePlayer ()
  {
    this.storedVelocity = this.GetComponent<Rigidbody> ().velocity;
    this.GetComponent<Rigidbody>().freezeRotation = true;
    this.GetComponent<Rigidbody>().isKinematic = true;
    this.enabled = false;
  }

  // Make the player unable to move
  public void UnFreezePlayer ()
  {
    this.GetComponent<Rigidbody> ().velocity = this.storedVelocity;
    this.GetComponent<Rigidbody>().freezeRotation = false;
    this.GetComponent<Rigidbody>().isKinematic = false;
    this.enabled = true;
  }
}