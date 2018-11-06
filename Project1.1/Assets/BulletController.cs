using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletController : NetworkBehaviour {
    private Transform trans2D;
    float Timer = 0.0f;
    float live = 0;

    int i = 0;

    public GameObject explo;


    void Start()
    {
        if (!isLocalPlayer)
            return;
        trans2D = GetComponent<Transform>();
    }
    float speed = 10.0f;
	// Update is called once per frame
    [Server]
	void Update () {
        if (!isLocalPlayer)
        {
            Timer += Time.deltaTime;

            if (Timer > 1.2 && live == 0)
            {
                CmdSpawnExplo();
                NetworkServer.Destroy(gameObject);
                live = 1;
            }
        } else
        {
            NetworkServer.Destroy(gameObject);
            Destroy(gameObject);
        }
	}

    [Command]
    void CmdSpawnExplo()
    {
        GameObject temp_explo = Instantiate(explo, transform.position, Quaternion.identity);

        NetworkServer.Spawn(temp_explo);
    }
}
