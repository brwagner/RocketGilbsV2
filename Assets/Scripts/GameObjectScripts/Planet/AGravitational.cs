using UnityEngine;
using System.Collections;

public abstract class AGravitational : BaseBehaviour, IGravitational {
  public float weight;
  public abstract Vector3 GetForceOnPoint (Vector3 position);
}