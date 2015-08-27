using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class ThrustInput : BaseBehaviour
{

  private EventTrigger eventTrigger;
  
  void Awake ()
  {
    this.eventTrigger = this.GetComponent<EventTrigger> ();
  }

  void Start ()
  {
    this.eventTrigger.triggers.Add (EventUtils.CreateEventEntry (EventTriggerType.PointerDown, (eventData) => UIEvent.Thrust ()));
    this.eventTrigger.triggers.Add (EventUtils.CreateEventEntry (EventTriggerType.PointerUp, (eventData) => UIEvent.Stop ()));
  }

  void Update ()
  {
    if (Input.GetKeyDown (KeyCode.UpArrow)) {
      UIEvent.Thrust ();
    } else if (Input.GetKeyUp (KeyCode.UpArrow)) {
      UIEvent.Stop ();
    }
  }
}