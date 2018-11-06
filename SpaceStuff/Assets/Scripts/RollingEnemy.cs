using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingEnemy : MonoBehaviour {
    public float speed = 3f;
    public bool rotY = false;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0,speed,0);
        if (rotY)
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, speed);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
