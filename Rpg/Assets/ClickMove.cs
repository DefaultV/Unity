using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickMove : MonoBehaviour {
    NavMeshAgent agent;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.DrawRay(transform.position, hit.point, Color.green);
                agent.SetDestination(hit.point);
            }
        }
    }
}
