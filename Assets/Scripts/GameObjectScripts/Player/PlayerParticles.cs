using UnityEngine;
using System.Collections;

// Particle systems used by player
public class PlayerParticles : BaseBehaviour {

  public ParticleSystem[] stopExhaust;
  public ParticleSystem goExhaust;
	
  void Awake() {
    UIEvent.OnThrust += EmitGoExhaust;
    UIEvent.OnStop += StopAllExhaust;
    UIEvent.OnBrake += EmitStopExhaust;
    GameEvent.OnPlayerCrash += StopAllExhaust;
  }
  
  void OnDestroy() {
    UIEvent.OnThrust -= EmitGoExhaust;
    UIEvent.OnStop -= StopAllExhaust;
    UIEvent.OnBrake -= EmitStopExhaust;
    GameEvent.OnPlayerCrash -= StopAllExhaust;
  }

  void Start() {
    goExhaust.playOnAwake = true;
    goExhaust.enableEmission = false;
    foreach (ParticleSystem system in stopExhaust) {
      system.playOnAwake = true;
      system.enableEmission = false;
    }
  }

  void EmitGoExhaust ()
  {
    goExhaust.enableEmission = true;
  }

  void EmitStopExhaust () {
    foreach (ParticleSystem system in stopExhaust) {
      system.enableEmission = true;
      system.startSpeed = Mathf.Min(this.GetComponent<Rigidbody>().velocity.magnitude, 3);
    }
  }

  void StopAllExhaust() {
    goExhaust.enableEmission = false;
    foreach (ParticleSystem system in stopExhaust) {
      system.enableEmission = false;
    }
  }
}