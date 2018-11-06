using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class playerIDcontroller : NetworkBehaviour {

    [SyncVar] string playerUniqueIdentity;
    public NetworkInstanceId playerNetID;

    public override void OnStartLocalPlayer()
    {
        GetNetIdentity();
        SetIdentity();
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.name == "" || transform.name == "CameraMov(Clone)")
        {
            SetIdentity();
        }
	}

    [Client]
    void GetNetIdentity()
    {
        playerNetID = GetComponent<NetworkIdentity>().netId;
        CmdTellServerMyIdentity(MakeUniqueIdentity());
    }

    void SetIdentity()
    {
        if (!isLocalPlayer)
        {
            transform.name = playerUniqueIdentity;
        }
        else
        {
            transform.name = MakeUniqueIdentity();
        }
    }

    string MakeUniqueIdentity()
    {
        return playerNetID.ToString();
    }

    [Command]
    void CmdTellServerMyIdentity(string id)
    {
        playerUniqueIdentity = id;
    }
}
