using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour {

    public float mouseSensitivity = 5.0f;
    private Camera playerCamera;
    private Transform crosshair;

    private GameObject hoveredObject;

    private EventManager eventManager;

	// Use this for initialization
	void Start () {
        this.eventManager = GameObject.Find("Main").GetComponent<EventManager>();
        playerCamera = GetComponentInChildren<Camera>();
        crosshair = GetComponentInChildren<Transform>();
        hoveredObject = null;
	}
	
	// Update is called once per frame
	void Update () {
        float leftRightRot = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, leftRightRot, 0f);

        float upDownRot = -Input.GetAxis("Mouse Y") * mouseSensitivity;
        playerCamera.transform.Rotate(upDownRot, 0f, 0f);

        RaycastHit hit;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward * 10f);

        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            hoveredObject = hit.collider.gameObject;
        }
        else
        {
            hoveredObject = null;
        }

        if (hoveredObject != null && Input.GetMouseButtonDown(0))
        {
            eventManager.Emit(MenuEvent.PRIMARY_CLICKED, hoveredObject);
        }

        if (hoveredObject != null && Input.GetMouseButtonDown(1))
        {
            eventManager.Emit(MenuEvent.SECONDARY_CLICKED, hoveredObject);
        }
    }
}
