using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsController : MonoBehaviour {
    public Transform[] wps;
    public Transform target;
    DistanceCalc wp;
    int inc = 0;

    // Use this for initialization
    void Start () {
        wp = GameObject.FindGameObjectWithTag("waypoint").GetComponent<DistanceCalc>();
        SetupWaypoints();

    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate((target.position - transform.position).normalized * 8f * Time.deltaTime, Space.World);
        transform.LookAt(target);
        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
            NextTarget();
    }

    void NextTarget()
    {
        inc++;
        if (inc >= wps.Length)
        {
            Destroy(gameObject);
            return;
        }
        target = wps[inc];
    }

    void SetupWaypoints()
    {
        wps = wp.wps;
        target = wp.target;
        inc = wp.newIndex;
    }
    
}
