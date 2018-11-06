using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpearSpawn : NetworkBehaviour {

    public GameObject Spear;
    public AudioClip Swing;
    float Cooldown;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        Cooldown += Time.deltaTime;
        if (!isLocalPlayer)
            return;
        if (Input.GetKeyDown(KeyCode.Mouse0) && Cooldown >= 0)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(Swing);
            CmdSpawnSpear();
            Cooldown = -2.0f;
        }
    }

    [Command]
    void CmdSpawnSpear()
    {
        GameObject temp_spear = Instantiate(Spear, transform.position - transform.forward * 1.0f, transform.rotation);
        temp_spear.GetComponent<Rigidbody>().AddForce(transform.forward * -50000.0f);

        NetworkServer.Spawn(temp_spear);
    }
}
