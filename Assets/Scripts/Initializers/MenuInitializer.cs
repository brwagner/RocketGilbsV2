using UnityEngine;
using System.Collections;
using System;

public class MenuInitializer : AbstractInitializer {
  protected override void PostStart() {
    GameEvent.MenuInitialized ();
  }
}