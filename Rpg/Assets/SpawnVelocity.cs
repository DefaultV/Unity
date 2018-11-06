using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVelocity : MonoBehaviour {

    Quaternion spawnRotation;
    Transform Player;
    public AudioClip Pickup;
	// Use this for initialization
	void Start () {
        Force();

    }
	
	// Update is called once per frame
	void Update () {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        PickUp();
    }
    void Force()
    {
        spawnRotation = new Quaternion(Random.Range(0, 90), 45f, Random.Range(0, 40), 0);
        GetComponent<Rigidbody>().AddForce(transform.forward*1f);
    }
    void PickUp()
    {
        if (Vector3.Distance(transform.position,Player.position) < 1f)
        {
            //Debug.Log(Vector3.Distance(transform.position, Player.position));
			Player.GetComponent<AudioSource>().PlayOneShot(Pickup);
            Destroy(gameObject);
        }
    }
}
