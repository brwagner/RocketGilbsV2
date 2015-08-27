using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using UnityEngine.UI;

public static class EventUtils {
  public static EventTrigger.Entry CreateEventEntry(EventTriggerType triggerType, UnityEngine.Events.UnityAction<BaseEventData> cb) {
    //Create a new entry. This entry will describe the kind of event we're looking for
    // and how to respond to it
    EventTrigger.Entry entry = new EventTrigger.Entry();
    
    //This event will respond to a drop event
    entry.eventID = triggerType;
    
    //Create a new trigger to hold our callback methods
    entry.callback = new EventTrigger.TriggerEvent();
    
    //Create a new UnityAction, it contains our DropEventMethod delegate to respond to events
    UnityEngine.Events.UnityAction<BaseEventData> callback = cb;
    
    //Add our callback to the listeners
    entry.callback.AddListener(callback);
    
    //Add the EventTrigger entry to the event trigger component
    return entry;
  }
}
