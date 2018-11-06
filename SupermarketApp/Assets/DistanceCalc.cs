using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalc : MonoBehaviour {
    public Transform[] wps;
    public Transform target;

    // Use this for initialization
    void Start () {
        SetupArrow();
        wpsDist = new float[wps.Length];
    }
	
	// Update is called once per frame
	void Update () {
        target = ClosestToPlayer();
	}

    void SetupArrow()
    {
        wps = new Transform[GameObject.Find("Waypoints").transform.childCount];
        for (int i = 0; i < wps.Length; i++)
        {
            wps[i] = GameObject.Find("Waypoints").transform.GetChild(i);
        }

        target = wps[0];
    }

    public float[] wpsDist;
    public int newIndex;
    public Transform ClosestToPlayer()
    {
        //Will explain
        for (int i = 0; i < wps.Length; i++)
        {
            wpsDist[i] = Vector3.Distance(wps[i].position, GameObject.FindGameObjectWithTag("Player").transform.position);
        }

        float f_ret = wpsDist[0];
        int index = 0;
        for (int i = 0; i < wps.Length; i++)
        {
            if (f_ret > wpsDist[i])
            {
                f_ret = wpsDist[i];
                index = i;
            }
        }
        newIndex = index;
        return wps[index];
    }
}
