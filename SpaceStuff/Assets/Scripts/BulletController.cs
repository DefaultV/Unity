using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    float Timer = 3f;
    public GameObject rocketSmokeObject;
    public GameObject rocketLaunchSmoke;
    public GameObject Brenderer;
    public AudioClip explosion;
    bool dead = false;
    Vector3 trigPos;
	// Use this for initialization
	void Start () {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y-3f, -1);
	}
	
	// Update is called once per frame
	void Update () {
        if (!dead)
            gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 1.5f);
        else
            transform.position = trigPos;
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            Destroy(gameObject);
        }
	}

    //FOR ENEMIES
    void OnTriggerEnter(Collider coll)
    {
        if (dead)
            return;
        Debug.Log(coll.gameObject.name);
        if (coll.gameObject.name == "Boss1")
            coll.gameObject.GetComponent<LazerBossScript>().meAsEnemy.ReduceHealth(200);
        else
            coll.transform.gameObject.GetComponent<EnemyController>().meAsEnemy.ReduceHealth(300);
        //coll.transform.gameObject.transform.FindChild("Enemy1").GetComponent<MeshRenderer>().materials[0].color = Color.red;
        trigPos = transform.position;
        Brenderer.GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        rocketSmokeObject.GetComponent<ParticleSystem>().Play();
        rocketLaunchSmoke.GetComponent<ParticleSystem>().Stop();
        GetComponent<AudioSource>().PlayOneShot(explosion);
        dead = true;
    }
}
