using UnityEngine;
using System.Collections;

public class GameInputManager : Singleton<GameInputManager>
{

  // Input variable that can be accessed from other classes
  public float Acceleration { set; get; }

  void Awake ()
  {
    UIEvent.OnThrust += Thrust;
    UIEvent.OnBrake += Brake;
    UIEvent.OnStop += Stop;
  }

  private void Brake ()
  {
    Acceleration = -1;
  }

  private void Thrust ()
  {
    Acceleration = 1;
  }

  private void Stop ()
  {
    Acceleration = 0;
  }

  void Update ()
  {
    GetInputFromKeys ();
  }

  private void GetInputFromKeys ()
  {
    if (Input.GetKeyDown (KeyCode.Space)) {
      LevelManager.Instance.LoadCurrentGameLevel ();
    } else if (Input.GetKeyDown (KeyCode.N)) {
      LevelManager.Instance.LoadNextGameLevel ();
    } else if (Input.GetKeyDown (KeyCode.P)) {
      LevelManager.Instance.LoadPreviousGameLevel ();
    }
  }
}