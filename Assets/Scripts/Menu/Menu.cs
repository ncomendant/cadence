using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    private SteamVR_TrackedController controller;
    private bool triggerDown = false;

    private EventManager eventManager;

    private void Awake()
    {
        eventManager = new EventManager();
        GameObject rig = GameObject.Find("[CameraRig]");
        controller = rig.transform.Find("Controller (right)").GetComponent<SteamVR_TrackedController>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (controller.triggerPressed)
        {
            if (!triggerDown)
            {
                triggerDown = true;
                eventManager.Emit(MenuEvent.TRIGGER_CLICKED);
            }
        } else
        {
            if (triggerDown)
            {
                triggerDown = false;
            }
        }
	}

    public void Emit(string eventName, params object[] data)
    {
        eventManager.Emit(eventName, data);
    }

    public void On(string eventName, EventHandler handler)
    {
        eventManager.On(eventName, handler);
    }
}
