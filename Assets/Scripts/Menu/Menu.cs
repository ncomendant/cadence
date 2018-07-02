using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Menu : MonoBehaviour {

    private SteamVR_TrackedController controller;
    private bool triggerDown = false;
    public bool vrEnabled = false;

    private EventManager eventManager;

    private void Awake()
    {
        eventManager = GetComponent<EventManager>();

        XRSettings.enabled = XRDevice.isPresent && (XRDevice.model == "HTC Vive" || XRDevice.model == "Vive MV") && vrEnabled;
        if (XRSettings.enabled)
        {
            GameObject.Find("FPS Character").SetActive(false);
            GameObject rig = GameObject.Find("[CameraRig]");
            controller = rig.transform.Find("Controller (right)").GetComponent<SteamVR_TrackedController>();
        } else
        {
            GameObject.Find("Menu VR Character").SetActive(false);
        }
    }

    void Start () {
        Cursor.visible = false;
	}
	
	void Update () {
        if (XRSettings.enabled)
        {
            if (controller.triggerPressed)
            {
                if (!triggerDown)
                {
                    triggerDown = true;
                    eventManager.Emit(MenuEvent.PRIMARY_CLICKED);
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
