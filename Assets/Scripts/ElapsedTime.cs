using UnityEngine;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class ElapsedTime : MonoBehaviour
{
    class TimeEvent
    {
        public float executionTime;
        public UnityAction action;
    }

    float elapsed;
    int minutes, seconds, milliseconds;
    [SerializeField]
    TMP_Text hud;

    Queue<TimeEvent> timeEvents = new Queue<TimeEvent>();
    TimeEvent nextEvent;

    void Update()
    {
        elapsed += Time.deltaTime;
        minutes = (int)(elapsed / 60) % 60;
        seconds = (int)(elapsed % 60);
        milliseconds = (int)(elapsed * 100) % 100;
        if (hud!=null) hud.text = string.Format("Mission Time: {0:D2}'{1:D2}''{2:D2}", minutes, seconds, milliseconds);
        HandleTimedEvents();
    }

    void HandleTimedEvents()
    {
        if (nextEvent == null && timeEvents.Count > 0)
        {
            nextEvent = timeEvents.Dequeue();
        }

        if (nextEvent != null)
        {
            if (elapsed > nextEvent.executionTime)
            {
                nextEvent.action.Invoke();
                nextEvent = null;
            }
        }
    }

    public void QueueEvent(float executionTime, UnityAction action) 
    {
        TimeEvent event1 = new TimeEvent();
        event1.executionTime = executionTime;
        event1.action = action;
        timeEvents.Enqueue(event1);
    }


}
