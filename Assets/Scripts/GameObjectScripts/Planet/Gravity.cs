using UnityEngine;
using System;
using System.Collections.Generic;

public class Gravity : AGravitational, IPersistable
{
  // Update is called once per frame
  void FixedUpdate ()
  {
    Game.Player.GetComponent<Rigidbody>().AddForce (GetForceOnPoint(Game.Player.transform.position));
  }

  public override Vector3 GetForceOnPoint(Vector3 position) {
    Vector3 displacement = this.transform.position - position;
    float distance = displacement.magnitude;
    return displacement * weight / (Mathf.Pow(distance, 3));
  }
}