using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotate : MonoBehaviour {

    Quaternion rotation;
    void Awake()
    {
        rotation = transform.rotation;
    }
    void LateUpdate()
    {
        transform.rotation = rotation;
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
