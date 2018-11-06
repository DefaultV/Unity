using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterStats : MonoBehaviour {
	public int ballCheck = 0;
	private bool dead = false;
	private NavMeshAgent agent;

    // Use this for initialization
    void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Input.GetKey(KeyCode.Mouse0))
		{
			if (Physics.Raycast(ray, out hit, 100))
			{
				Debug.DrawRay(transform.position, hit.point, Color.green);
				if (hit.collider.gameObject.tag == "Sheep" && ballCheck == 1 && hit.collider.gameObject.name != "dead") {
					GameObject.FindGameObjectWithTag ("Ball").SendMessage("ThrowBall", hit.collider.gameObject);
					Debug.Log (hit.collider.gameObject.tag);
					ballCheck = 0;
				}
			}
		}
	}
	void BallGet(){
		Debug.Log ("ballget");
		if (dead) {
			GameObject.FindGameObjectWithTag ("Ball").SendMessage ("caught");
		} else {
			GameObject.FindGameObjectWithTag ("Enemy").SendMessage ("Me", gameObject);
			ballCheck = 1;
		}
	}
	void caught(){
		gameObject.name = "dead";
		if (dead)
			return;
		tag.Remove (0);
		GetComponent<Light> ().intensity = 6.0f;
		dead = true;
		agent.Stop (true);
	}
}
