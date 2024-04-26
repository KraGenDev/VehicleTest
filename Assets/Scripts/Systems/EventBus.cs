using System;
using System.Collections.Generic;
using Enums;

public class EventBus
{
    private Dictionary<EventBusAction, List<Action>> _listeners = new Dictionary<EventBusAction, List<Action>>();
    
    private static EventBus _instance;
    private EventBus() {}

    public static EventBus Instance => _instance ??= new EventBus();

    public void Subscribe(EventBusAction eventName, Action listener)
    {
        if (!_listeners.ContainsKey(eventName))
        {
            _listeners[eventName] = new List<Action>();
        }

        _listeners[eventName].Add(listener);
    }

    public void Unsubscribe(EventBusAction eventName, Action listener)
    {
        if (_listeners.ContainsKey(eventName))
        {
            _listeners[eventName].Remove(listener);
        }
    }

    public void Emit(EventBusAction eventName)
    {
        if (_listeners.ContainsKey(eventName))
        {
            foreach (Action listener in _listeners[eventName].ToArray())
            {
                listener();
            }
        }
    }
}