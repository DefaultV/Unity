using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvSpawner : MonoBehaviour {

    public GameObject envObject;

    //float moveSpeed = 5f;
    public float spawnTimer = 4f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        SpawnLoop();
	}

    void SpawnLoop()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            GameObject tmp = Instantiate(envObject);
            tmp.transform.position = gameObject.transform.position;
            tmp.transform.localScale = new Vector3(Rnd(), Rnd(), Rnd());
            //Vector3 dir = transform.position - new Vector3(transform.position.x, transform.position.y - 20f, -1);
            //tmp.transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);
            spawnTimer = Rnd2();
        }
    }

    float Rnd2()
    {
        return Random.Range(6f, 11f);
    }

    float Rnd()
    {
        return Random.Range(1f, 3f);
    }
}
