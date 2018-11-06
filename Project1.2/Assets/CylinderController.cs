using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderController : MonoBehaviour {

    Transform CylinderTransform;
    float X;
    float Y;
    float Z;
	// Use this for initialization
	void Start () {
        CylinderTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.W))
        {
            X += 1;
        }

        CylinderTransform.position = new Vector3(X, Y, Z);
    }
}
