using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public float range;
    public Canvas UI;

    Transform Player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        RangeClose();
	}
    void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, Player.position) < 5f)
        {
            UI.enabled = true;
            UI.scaleFactor = 500f;
        }
    }
    void RangeClose()
    {
        if (Vector3.Distance(transform.position, Player.position) > 5f)
        {
            UI.enabled = false;
            //Debug.Log("???" + UI.enabled);
        }
    }
}
