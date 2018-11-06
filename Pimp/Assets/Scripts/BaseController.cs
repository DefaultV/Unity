using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BaseController : NetworkBehaviour {

    public GameObject peonPrefab;
    public string baseNetID;
	// Use this for initialization
	void Start () {
        CmdSpawnPeon();
	}
	
	// Update is called once per frame
	void Update () {
	}

    [Command]
    void CmdSpawnPeon()
    {
        GameObject instance = Instantiate(peonPrefab);
        Vector3 spawnPos = transform.GetChild(0).position;

        instance.transform.position = spawnPos;
        instance.GetComponent<UnitController>().PlayerID = GetMyIdentity();

        NetworkServer.Spawn(instance);
    }

    [Client]
    string GetMyIdentity()
    {
        return baseNetID;
    }
}
