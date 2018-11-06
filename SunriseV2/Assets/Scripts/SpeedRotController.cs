using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedRotController : MonoBehaviour {

    public float speed = 2f;
    public float maxspeed = 10f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W) && transform.localPosition.z <= maxspeed)
            transform.localPosition += Vector3.forward * Time.deltaTime * speed;
	}
}
