using UnityEngine;
using System.Collections;
using System;

public class GameInitializer : AbstractInitializer {
 	protected override void PostStart () {
    GameEvent.GameInitialized ();
	}
}