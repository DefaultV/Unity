using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotate : MonoBehaviour {

    public float rotateSpeed;
	void Start()
    {
        rotateSpeed = 0.3f;
    }
	// Update is called once per frame
	void Update () {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotateSpeed);
	}
}
