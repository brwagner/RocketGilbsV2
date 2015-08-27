using UnityEngine;
using System.Collections;

public interface IGravitational {
  Vector3 GetForceOnPoint(Vector3 position);
}