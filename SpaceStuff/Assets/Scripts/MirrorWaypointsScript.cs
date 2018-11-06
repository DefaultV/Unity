using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorWaypointsScript : MonoBehaviour {

    List<GameObject> allWaypoints = new List<GameObject>();
	// Use this for initialization
	void Start () {
        foreach (GameObject waypoint in GameObject.FindObjectsOfType<GameObject>())
        {
            if (waypoint.tag == "Waypointtag")
                allWaypoints.Add(waypoint);
        }
	}

    // Update is called once per frame
    bool done = false;
	void Update () {
        if (done)
            return;
		if (GameObject.Find("Player").GetComponent<PlaneController>().mainPlayer.GetScore() >= 300)
        {
            foreach (GameObject waypoint in allWaypoints)
            {
                waypoint.transform.position = new Vector3(waypoint.transform.position.x * -1f, waypoint.transform.position.y*-1f, waypoint.transform.position.z);
            }
            done = true;
        }
	}
}
