using UnityEngine;
using System.Collections;

public class AntiGravity : AGravitational, IPersistable {

  // Update is called once per frame
  void FixedUpdate ()
  {
    Game.Player.GetComponent<Rigidbody>().AddForce (GetForceOnPoint(Game.Player.transform.position));
  }
  
  public override Vector3 GetForceOnPoint(Vector3 position) {
    Vector3 displacement = position - this.transform.position;
    float distance = displacement.magnitude;
    return displacement * weight / (Mathf.Pow(distance, 3));
  }
}