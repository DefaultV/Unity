using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	// Use this for initialization
	int startCount = 0;
	List<GameObject> sheeps = new List<GameObject>();
	void Start () {
		foreach (GameObject sheep in GameObject.FindGameObjectsWithTag("Sheep")) {
			startCount++;
			sheeps.Add (sheep);
		}
	}
	
	// Update is called once per frame
	int count = 0;

	void Update () {
		foreach (GameObject sheep in sheeps) {
			if (sheep.name == "dead") {
				count++;
				sheeps.Remove (sheep);
			}
		}
		Debug.Log (count +"  "+ startCount);
		if (startCount - 1 == count)
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
			
	}
}
