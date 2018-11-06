using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLeft : MonoBehaviour {
	public KeyCode LeftArrow;
	public KeyCode RightArrow;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (LeftArrow)) {
			rLeft();
		}

		if (Input.GetKey (RightArrow)) {
			rRight();
		}
	}

	void rLeft(){
		transform.localRotation = Quaternion.Euler(new Vector3(0,-180,0));
	}
	void rRight(){
		transform.localRotation = Quaternion.Euler (new Vector3 (0,0,0));
	}

}
