using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Menu : MonoBehaviour {

    private SteamVR_TrackedController controller;
    private bool triggerDown = false;

    private EventManager eventManager;

    private void Awake()
    {
        eventManager = GetComponent<EventManager>();

        Settings.vrEnabled = XRDevice.isPresent && XRDevice.model == "HTC Vive";
        if (Settings.vrEnabled)
        {
            GameObject rig = GameObject.Find("[CameraRig]");
            controller = rig.transform.Find("Controller (right)").GetComponent<SteamVR_TrackedController>();
        }
    }

    // Use this for initialization
    void Start () {
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Settings.vrEnabled)
        {
            if (controller.triggerPressed)
            {
                if (!triggerDown)
                {
                    triggerDown = true;
                    eventManager.Emit(MenuEvent.SECONDARY_CLICKED);
                }
            }
            else
            {
                if (triggerDown)
                {
                    triggerDown = false;
                }
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
