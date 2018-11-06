using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControllerJoy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("GunHorizontal");
        float y = Input.GetAxis("GunVertical");

        if (x < 0.08f && x > -0.08f && y < 0.08f && y > -0.08f)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.localEulerAngles = new Vector3(Mathf.Atan2(Input.GetAxis("GunVertical"), Input.GetAxis("GunHorizontal")) * 180 / Mathf.PI + 90, 0, 0);
        }
    }
}
