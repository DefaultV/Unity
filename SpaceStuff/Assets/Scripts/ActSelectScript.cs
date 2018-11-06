using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActSelectScript : MonoBehaviour {

    GameObject Player;

	// Use this for initialization
	void Start () {
        Player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(gameObject.transform.position, Player.transform.position) <= 6f)
        {
            switch (gameObject.name)
            {
                case "Act1Portal":
                    SceneManager.LoadScene("Scene111");
                    break;
                case "Act2Portal":
                    SceneManager.LoadScene("Scene222");
                    break;
                case "OutpostPortal":
                    SceneManager.LoadScene("Outpost");
                    break;
                case "ActSelection":
                    SceneManager.LoadScene("ActSelection");
                    break;
                default:
                    break;
            }
        }

	}
}
