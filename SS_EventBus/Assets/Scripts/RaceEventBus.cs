using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaceEventBus
{
    //This is our event bus script

    //This Events directory is the key ingredient for our RaceEventBus class
    //This will act as a ledger in which we maintain a list of relations between
    //  event types and subscribers
    //We keep it private and as a readonly to ensure that it can't be 
    //  overwritten
    private static readonly
        IDictionary<RaceEventType, UnityEvent>
        Events = new Dictionary<RaceEventType, UnityEvent>();

    //The client of the dictionary must call this method in order to add itself
    //  as a subscriber of a specific event type
    //The two parameters are the race event type and the callback function
    //UnityAction is a delegate type so it provides a way for us to pass a 
    //  method as an argument
    public static void Subscribe(RaceEventType eventType, UnityAction listener)
    {
        UnityEvent thisEvent;

        if(Events.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Events.Add(eventType, thisEvent);
        }
    }

    //This method permits objects to remove themselves as subscribers
    //  of a specific event type.
    public static void Unsubscribe(RaceEventType type, UnityAction listener)
    {
        UnityEvent thisEvent;

        if(Events.TryGetValue(type, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    //When a client object calls this method, the registered callback methods
    //  of all subscribers of a specific race event type will get called at the 
    //  same time.
    public static void Publish(RaceEventType type)
    {
        UnityEvent thisEvent; 

        if(Events.TryGetValue(type, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}
