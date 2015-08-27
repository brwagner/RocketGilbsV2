using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Tutorial : BaseBehaviour {

  private bool shouldMoveNext = false;
  public Text textBox;

	void Start() {
    StartCoroutine (DoTutorialRoutine ());
  }

  private IEnumerator DoTutorialRoutine() {
    yield return new WaitForSeconds(1f);
    yield return DisplayTextAndWaitForResponse (TutorialText [0]);
    yield return new WaitForSeconds(0.9f);
    yield return DisplayTextAndWaitForResponse (TutorialText [1]);
    yield return new WaitForSeconds(0.8f);
    yield return DisplayTextAndWaitForResponse (TutorialText [2]);
    UIEvent.Thrust ();
    yield return new WaitForSeconds(0.2f);
    UIEvent.Stop ();
    yield return new WaitForSeconds(0.2f);
    yield return DisplayTextAndWaitForResponse (TutorialText [3]);
    UIEvent.Brake ();
    yield return new WaitForSeconds(0.4f);
    UIEvent.Stop ();
    yield return new WaitForSeconds(1f);
    UIEvent.Brake ();
    textBox.text = TutorialText[4];
    yield return new WaitForSeconds(2f);
    textBox.text = TutorialText [5];
    yield return new WaitForSeconds (5f);
    textBox.text = TutorialText [6];
    yield return new WaitForSeconds (1f);
    UIEvent.Stop ();
    GameActionsManager.Instance.LoadApplicationLevel (0);
  }

  public void MoveNext() {
    shouldMoveNext = true;
  }

  private Coroutine DisplayTextAndWaitForResponse(string text) {
    return StartCoroutine (DoDisplayTextAndWaitForResponse (text));
  }

  private IEnumerator DoDisplayTextAndWaitForResponse(string text) {
    textBox.text = text;
    yield return StartCoroutine (DoWaitForResponse ());
  }

  private IEnumerator DoWaitForResponse() {
    Time.timeScale = 0;
    yield return WaitUntil(() => shouldMoveNext);
    Time.timeScale = 1;
    shouldMoveNext = false;
  }

  private string[] TutorialText = new string[] {
    "YOU NEVER LEARNED HOW TO STEER THE SHIP. BUT YOU CAN TURN USING GRAVITY",
    "COLLECTING DISTRESS SIGNALS WILL LET YOU KEEP LOOKING AT YOUR PROJECTED PATH",
    "THRUSTING WILL MAKE YOUR ORBIT BIGGER\nTHRUST BY PRESSING THE RIGHT SIDE OF THE SCREEN",
    "BRAKING WILL MAKE YOUR ORBIT SMALLER\nBRAKE BY PRESSING THE LEFT SIDE OF THE SCREEN",
    "BE CAREFUL NOW!",
    "COLLECT ALL OF THE DISTRESS SIGNALS TO ADVANCE TO THE NEXT LEVEL",
    "GOOD LUCK!"
  };
}
