using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUDropController : MonoBehaviour {

    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= 1.5f)
        {
            if (gameObject.name == "PU-Rockets(Clone)")
                player.GetComponent<PlaneController>().mainPlayer.SetRockets(player.GetComponent<PlaneController>().mainPlayer.GetRockets() + 5);
            if (gameObject.name == "PU-Lazer(Clone)")
                player.GetComponent<PlaneController>().mainPlayer.SetLazer(player.GetComponent<PlaneController>().mainPlayer.GetLazer() + 1);
            if (gameObject.name == "PU-PlusScore(Clone)")
                player.GetComponent<PlaneController>().mainPlayer.UpdateScore(10);
            Destroy(gameObject);
        }
	}
}
