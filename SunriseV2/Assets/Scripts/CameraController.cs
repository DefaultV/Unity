using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float mouseSensitivity = 2f;

    float rotX;
    float rotY;

    bool updateLook = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateLook();
    }

    void UpdateLook()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
            updateLook = true;
        if (Input.GetKeyUp(KeyCode.Mouse1))
            updateLook = false;

        if (!updateLook)
            return;

        rotX = Input.GetAxis("Mouse X") * mouseSensitivity;
        rotY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(0, -rotX, 0);
        transform.Rotate(rotY, 0, 0);
    }
}
