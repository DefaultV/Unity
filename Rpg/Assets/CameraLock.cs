using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour {
    Vector3 Player;
    // Use this for initialization
    void Start () {
		Player = GetComponent<Transform>().position;
        Camera.main.transform.position = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
        Player = GetComponent<Transform>().position;
        LockOn();
	}

    void LockOn()
    {
        Camera.main.transform.position = new Vector3(Player.x +2f, Player.y + 8f, Player.z - 5f);
    }
}
