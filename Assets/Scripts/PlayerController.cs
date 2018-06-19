using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float mouseSensitivity = 5.0f;
    private Camera camera;

	// Use this for initialization
	void Start () {
        camera = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        float leftRightRot = Input.GetAxis("Mouse X") * mouseSensitivity;
        //transform.Rotate(0, leftRightRot, 0f);

        float upDownRot = -Input.GetAxis("Mouse Y") * mouseSensitivity;
        //camera.transform.Rotate(upDownRot, 0f, 0f);
    }
}
