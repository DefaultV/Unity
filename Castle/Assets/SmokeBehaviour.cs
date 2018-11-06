using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBehaviour : MonoBehaviour {
	// Use this for initialization
	void Start () {
		Destroy (this, 1.1f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale.Set(transform.localScale.x * 0.7f, transform.localScale.y * 0.7f, transform.localScale.z * 0.7f);
		GetComponent<SpriteRenderer>().color = new Color(255,255,255,GetComponent<SpriteRenderer>().color.a * 0.96f);
		Destroy (this, 1.1f);
	}
}
