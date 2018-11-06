using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SheepController : MonoBehaviour {
	NavMeshAgent agent;
	AudioSource source;
	private bool dead = false;
	public int health = 20;
	public AudioClip sound_sheep;
	public int ballCheck = 0;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (dead)
			return;
		/*RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Input.GetKey(KeyCode.Mouse0))
		{
			if (Physics.Raycast(ray, out hit, 100))
			{
				Debug.DrawRay(transform.position, hit.point, Color.green);
				if (hit.collider.tag == "Sheep" && ballCheck == 1) {
					source.PlayOneShot (sound_sheep);
					GameObject.FindGameObjectWithTag ("Ball").SendMessage("ThrowBall", gameObject);
					ballCheck = 0;
				}
				s_hit = hit;
			}
		}*/

		if (isDragging) {
			DragAgent ();
			agent.SetDestination(s_hit.point);
		}

		if (Input.GetKeyUp (KeyCode.Mouse0)) {
			isDragging = false;
		}
			
		if (!isDragging) {
			rndVec = new Vector3 (Random.Range (-25f, 30f), 0.5f, Random.Range (-30f, 5f));
			if (!waiting) {
				m_timer = 0;
				m_timerEnd = Random.Range (5, 15);
				waiting = true;
			}
			m_timer += Time.deltaTime;

			if (m_timer > m_timerEnd) {
				resetAgent ();
				agent.SetDestination (rndVec);
				waiting = false;
			}
		}
	}
	private Vector3 rndVec;
	private float m_timer = 0;
	private float m_timerEnd;
	private bool waiting = false;
	private bool isDragging = false;
	private RaycastHit s_hit;
	void OnMouseDown()
	{
		
	}
	void resetAgent(){
		agent.speed = 10;
		agent.angularSpeed = 300;
		agent.acceleration = 40;
	}
	void DragAgent(){
		agent.speed = 100;
		agent.angularSpeed = 0;
		agent.acceleration = 10000;
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
		ballCheck = 0;
		gameObject.transform.localScale = new Vector3 (0,0,0);
		gameObject.name = "dead";
		if (dead)
			return;
		tag.Remove (0);
		GetComponent<Light> ().intensity = 6.0f;

		agent.Stop (true);
		Destroy (this);
		dead = true;
	}
}
