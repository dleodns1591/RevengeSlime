using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    Coin,
    Level,
}

public class EventManager : Singleton<EventManager>
{
    Dictionary<EventType, List<IListener>> dick = new Dictionary<EventType, List<IListener>>();

    public void AddLisetner(IListener listener, EventType type)
    {
        if (dick.ContainsKey(type))
        {
            dick[type].Add(listener);
        }
        else
        {
            dick.Add(type, new List<IListener>() { listener });
        }
    }

    public void EventPost(EventType type)
    {
        foreach (var item in dick[type])
        {
            item.Event(type);
        }
    }
}


public interface IListener
{
    void Event(EventType type);
}