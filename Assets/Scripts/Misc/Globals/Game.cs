using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public static class Game {

  private static GameObject player;

  public static GameObject Player {
    get {
      if (player == null) {
        player = GameObjectExtensions.FindGameObjectWithTag (Tag.Player);
      }
      return player;
    }
  }

  public static GameObject[] Signals {
    get {
      return GameObjectExtensions.FindGameObjectsWithTag (Tag.Signal);
    }
  }

  public static GameObject[] Planets {
    get {
      return GameObjectExtensions.FindGameObjectsWithTag(Tag.Planet);
    }
  }
}