using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    NavMeshAgent agent;
    Transform opponent;
	public GameObject light;
    public float searchRange = 5;
    public float Health;
    bool isAlive;
    public GameObject[] Loot;
    bool DroppedLoot;
	List<GameObject> opp_list = new List<GameObject> ();
	public static GameObject huntedSheep;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Health = 20f;
        isAlive = true;
    }

    // Update is called once per frame
	float overTimer = 1;
	float accTimer = 5;
	GameObject ball;
    void Update()
    {
		ball = huntedSheep;
		overTimer += Time.deltaTime*3;
		accTimer += Time.deltaTime;
		//INCREASE MOVEMENT
		agent.speed = overTimer;
		agent.acceleration = accTimer;
		foreach (GameObject sheep in opp_list) {
			if (Vector3.Distance (gameObject.transform.position, sheep.transform.position) < 5f && sheep.name != "dead"){
				sheep.SendMessage ("caught");
			}
		}
		if (Vector3.Distance (gameObject.transform.position, ball.transform.position) < 5f) {
			Debug.Log ("CAUGT");
			GameObject.FindGameObjectWithTag ("Ball").SendMessage ("caught");
		}

        if (!isAlive)
        {
            //agent.SetDestination(gameObject.transform.position);
            agent.enabled = false;
            if (!DroppedLoot) {
                GetComponent<Transform>().position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f, gameObject.transform.position.z);
                SpawnLoot();
                DroppedLoot = true;
            }
        }
        else
        {
            LookForPlayer();
            MoveToPlayer();
            HealthCheck();
        }
    }

    void MoveToPlayer()
    {
		int i = 0;
		//foreach (GameObject trans in opp_list) {
			//if (i == opp_list.Count - 2)
				//return;
			//if (Vector3.Distance (transform.position, opp_list[i].transform.position) < Vector3.Distance (transform.position, opp_list[i+1].transform.position)) {
		agent.SetDestination (huntedSheep.transform.position);
			//}
			i++;
		//}
		checkedPlayer = false;
    }
	bool checkedPlayer = false;
	void LookForPlayer()
	{
		opp_list.Clear ();
		foreach (GameObject trans in GameObject.FindGameObjectsWithTag("Sheep")) {
			opp_list.Add (trans);
		}
		checkedPlayer = true;
	}

    void OnMouseDown()
    {
    	//Health -= 10f;

    }
    void HealthCheck()
    {
        if (Health <= 0)
        {
			light.GetComponent<Light>().intensity = 0;
            isAlive = false;
            DroppedLoot = false;
        }
    }
    void SpawnLoot()
    {
        for (int i = 0; i<Loot.Length; i++)
        {
            if (Random.value > 0.0)
            {
                Vector3 lootPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                GameObject temp_loot = Instantiate(Loot[i], lootPos, Quaternion.identity);
            }
        }
    }
	void Me(object sheep){
		huntedSheep = (GameObject)sheep;
	}
}
