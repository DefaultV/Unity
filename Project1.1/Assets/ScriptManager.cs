using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptManager : MonoBehaviour {

    // Use this for initialization
    public ScriptManager playerScript;
    void Start () {
	}

    void Awake()
    {
        ScriptManager here = playerScript;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
