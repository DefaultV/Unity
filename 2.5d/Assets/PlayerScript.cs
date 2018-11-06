using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerScript : NetworkBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }
            
        Camera.main.transform.position = transform.position;
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        if (isLocalPlayer)
        {
            transform.rotation = new Quaternion(Camera.main.transform.rotation.x, 0, Camera.main.transform.rotation.z, 0);
            transform.Translate(x, 0, -z);
        }
    }
}
