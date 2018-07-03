using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    private Dictionary<string, List<EventHandler>> handlers = new Dictionary<string, List<EventHandler>>();

	public void On(string eventName, EventHandler handler)
    {
        if (!handlers.ContainsKey(eventName))
        {
            handlers[eventName] = new List<EventHandler>();
        }
        List<EventHandler> list = handlers[eventName];
        list.Add(handler);
    }

    public void Emit(string eventName, params object[] data)
    {
        if(handlers.ContainsKey(eventName))
        {
            List<EventHandler> list = handlers[eventName];
            for (int i = list.Count-1; i >= 0; i--)
            {
                list[i].OnEvent(data);
            }
        }
    }

    public void Remove(string eventName, EventHandler handler)
    {
        handlers[eventName].Remove(handler);
    }
}
