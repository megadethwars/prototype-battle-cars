using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus
{
    private static EventBus _instance;
    public static EventBus Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EventBus();
            }
            return _instance;
        }
    }

    private Dictionary<Type, Action<EventArgs>> eventDictionary = new Dictionary<Type, Action<EventArgs>>();

    public void Subscribe<T>(Action<EventArgs> listener) where T : EventArgs
    {
        if (!eventDictionary.ContainsKey(typeof(T)))
        {
            eventDictionary[typeof(T)] = delegate { };
        }
        eventDictionary[typeof(T)] += listener;
    }

    public void Unsubscribe<T>(Action<EventArgs> listener) where T : EventArgs
    {
        if (eventDictionary.ContainsKey(typeof(T)))
        {
            eventDictionary[typeof(T)] -= listener;
        }
    }

    public void TriggerEvent<T>(EventArgs e) where T : EventArgs
    {
        if (eventDictionary.ContainsKey(e.GetType()))
        {
            eventDictionary[e.GetType()].Invoke(e);
        }
    }
}
