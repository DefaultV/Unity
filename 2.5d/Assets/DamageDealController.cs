using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DamageDealController : NetworkBehaviour {

    public AudioClip Hurt;
    [SyncVar]
    public float Health = 100;

    Vector3 spawnPoint;
    private NetworkStartPosition[] spawnPoints;
    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update()
    {
        spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        RpcPlay();
    }

    [ServerCallback]
    void OnTriggerEnter(Collider other)
    {
        Health -= 50;
        RpcPlay();
        PlaySound();
    }


    [ClientRpc]
    void RpcPlay()
    {
        if (Health <= 0)
        {
            spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            transform.position = spawnPoint;
            Health = 100;
        }
        
    }
    void PlaySound()
    {
        GetComponent<AudioSource>().PlayOneShot(Hurt);
    }
}
