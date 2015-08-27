using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(EventTrigger))]
public class BrakeInput : BaseBehaviour
{

  private EventTrigger eventTrigger;

  void Awake ()
  {
    this.eventTrigger = this.GetComponent<EventTrigger> ();
  }

  void Start ()
  {
    this.eventTrigger.triggers.Add (EventUtils.CreateEventEntry (EventTriggerType.PointerDown, (eventData) => UIEvent.Brake ()));
    this.eventTrigger.triggers.Add (EventUtils.CreateEventEntry (EventTriggerType.PointerUp, (eventData) => UIEvent.Stop ()));
  }
  
  void Update ()
  {
    if (Input.GetKeyDown (KeyCode.DownArrow)) {
      UIEvent.Brake ();
    } else if (Input.GetKeyUp (KeyCode.DownArrow)) {
      UIEvent.Stop ();
    }
  }
}