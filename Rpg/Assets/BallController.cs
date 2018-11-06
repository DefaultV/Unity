using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
	GameObject curr_Sheep;
	public GameObject player_sheep_debug;
	// Use this for initialization
	void Start () {
		
	}
	float time = 0;
	float endTimerBall = 2;
	// Update is called once per frame
	List<GameObject> sheeps = new List<GameObject>();
	bool started = false;
	int s_count = 0;
	void Update () {
		time += Time.deltaTime;
		if (!started) {
			foreach (GameObject sheep in GameObject.FindGameObjectsWithTag("Sheep")) {
				sheeps.Add (sheep);
				s_count++;
			}
			curr_Sheep = sheeps [Random.Range (0, s_count + 1)]; // player_sheep_debug;
			if (curr_Sheep.name == "dead") {
				foreach (GameObject sheep in GameObject.FindGameObjectsWithTag("Sheep")) {
					if (sheep.name != "dead") {
						curr_Sheep = sheep;
					}
				}
			}
			curr_Sheep.SendMessage ("BallGet");
			started = true;
		}
		if (Vector3.Distance (gameObject.transform.position, curr_Sheep.transform.position) < 5f || time > endTimerBall) {
			GetComponent<Transform> ().position = new Vector3 (curr_Sheep.transform.position.x - 2, 4f, curr_Sheep.transform.position.z - 2);
			time = 0;
		} else
			GetComponent<Transform> ().position = Vector3.Lerp (gameObject.transform.position, (new Vector3 (curr_Sheep.transform.position.x, 2f, curr_Sheep.transform.position.z)), Time.deltaTime); //new Vector3 (curr_Sheep.transform.position.x-2, 4f,curr_Sheep.transform.position.z-2);	
	}
	void ThrowBall(object value){
		curr_Sheep = (GameObject)value;
		curr_Sheep.SendMessage ("BallGet");
	}
	void caught(){
		curr_Sheep.SendMessage ("caught");
		started = false;
	}
}
