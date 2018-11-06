using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotController : MonoBehaviour {

    Transform SpeedRotController;
	// Use this for initialization
	void Start () {
        SpeedRotController = GameObject.Find("SpeedRotController").transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = SpeedRotController.localPosition / 10;
	}
}
