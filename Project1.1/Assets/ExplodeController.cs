using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ExplodeController : NetworkBehaviour {
    public AudioSource source;
    public AudioClip Explosion;

    float Timer = 0.0f;
    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(Explosion);
    }
	
	// Update is called once per frame
    [ServerCallback]
	void Update () {
        Timer += Time.deltaTime;
        if (Timer > 1)
        {
            NetworkServer.Destroy(gameObject);
        }
	}
}
