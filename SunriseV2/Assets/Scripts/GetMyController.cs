using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMyController : MonoBehaviour {

    Transform controller;
    Transform rotController;
    public float distance;
    float speed = 5;
	// Use this for initialization
	void Start () {
        controller = GameObject.Find("CameraController").transform.FindChild("SpeedRotController");
    }

    // Update is called once per frame
    void Update()
    {
        MovementAndRotation();
    }

    void MovementAndRotation()
    {
        distance = Vector3.Distance(transform.position, controller.localPosition);
        GetComponent<Rigidbody>().AddForce(controller.position * Time.deltaTime * controller.localPosition.z);

        transform.LookAt(controller);
    }
}
