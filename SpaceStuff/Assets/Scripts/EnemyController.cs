using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public int health;
    public Enemy meAsEnemy;
    //GameObject player;
    float distanceToPlayer;

    private Transform[] points;
    private float speed = 4f;

    public Transform waypointTarget;
    private int wavepointIndex = 0;

    public float countdownToShoot = 5f;
    public AudioClip lazersound;
    public AudioClip bossexplosion;

    public GameObject particlePart;

    public GameObject loot;

    void Start()
    {
        meAsEnemy = new Enemy(health);
        /*if (gameObject.name == "Boss1")
        {
            waypointTarget = GameObject.Find("WaypointsBoss1").transform;
            meAsEnemy.bossHealth(true);
            canMove = false;
            return;
        }*/
        countdownToShoot = Random.Range(2f, 8f);
        //player = GameObject.Find("Player");
        //Waypoint
        points = new Transform[GameObject.Find("Waypoints" + gameObject.tag).transform.childCount];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = GameObject.Find("Waypoints" + gameObject.tag).transform.GetChild(i);
            
        }
        waypointTarget = points[0];
    }

    // Update is called once per frame
    bool canMove = true;
    bool bossArrived = false;
    void Update()
    {
        /*if (GameObject.Find("Player").GetComponent<PlaneController>().mainPlayer.GetScore() >= 300)
            canMove = true;*/
        GetComponentInChildren<MeshRenderer>().materials[0].color = Color.white;
        CheckHealthOfMe();
        Shoot();
        //waypoint
        Vector3 dir = waypointTarget.position - gameObject.transform.position;
        if (!bossArrived && canMove)
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, waypointTarget.position) <= 0.2f)
        {
            //Debug.Log("Boss arrived: " + bossArrived);
            if (gameObject.name == "Boss1")
                bossArrived = true;
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex == points.Length-1)
        {
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        waypointTarget = points[wavepointIndex];
    }


    void Shoot()
    {
        if (!canMove || gameObject.transform.position.z <= -1.2f)
            return;
        countdownToShoot -= Time.deltaTime;
        if (countdownToShoot <= 0)
        {
            particlePart.GetComponentInChildren<ParticleSystem>().Play();
            GetComponent<AudioSource>().PlayOneShot(lazersound);
            countdownToShoot = Random.Range(2f, 7f);
        }
    }


    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {
            //meAsEnemy.ReduceHealth(30);
            //Destroy(coll.gameObject);
        }
    }

    void OnParticleCollision(GameObject particleHolderObject)
    {
        if (particleHolderObject.name == "PowerUpBeamDeflector")
        {
            if (Random.Range(0, 2) == 1)
                meAsEnemy.ReduceHealth(200);
        }
        //{
        meAsEnemy.ReduceHealth(1);
        if (!GetComponent<ParticleSystem>().isPlaying && gameObject.name != "Boss1" && GetComponent<ParticleSystem>().gameObject.tag == "Enemy")
            GetComponent<ParticleSystem>().Play();
        if (Random.Range(0,2) == 1)
            GetComponentInChildren<MeshRenderer>().materials[0].color = Color.red;
        //}
    }

    public GameObject DeathParticles;
    void DisableMe()
    {
        LootRoll();
        if (gameObject.name == "Boss1")
            GameObject.Find("Environment").GetComponent<UIBehaviour>().countdown = true;
        GetComponentInChildren<BoxCollider>().enabled = false;
        gameObject.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<ParticleSystem>().Stop();
        gameObject.GetComponent<BoxCollider>().enabled = false;
        GetComponentInChildren<MeshRenderer>().enabled = false;
        GetComponentInChildren<Light>().enabled = false;
        gameObject.GetComponent<EnemyController>().enabled = false;
        GameObject tmp = Instantiate(DeathParticles);
        tmp.transform.position = transform.position;
    }
    bool dead = false;

    float beingDeathTimer = 3f;
    void CheckHealthOfMe()
    {
        if (meAsEnemy.GetHealth() <= 0)
        {
            beingDeathTimer -= Time.deltaTime;
            if (beingDeathTimer <= 0f)
            {
                DisableMe();
            }
            if (dead)
                return;
            //Debug.Log("DEAD!!!!!!!!");
            GameObject.Find("Player").GetComponent<PlaneController>().mainPlayer.UpdateScore(10);
            if (gameObject.name == "Boss1")
            {
                GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 0.2f;
                GetComponent<AudioSource>().volume = 0.5f;
                GetComponent<AudioSource>().PlayOneShot(bossexplosion);
                GameObject.Find("BossDeath").GetComponent<ParticleSystem>().Play();
                GameObject.Find("Flash").GetComponent<BossFlash>().begin = true;
                //UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
            else
            {
                DisableMe();
            }
            countdownToShoot = 100f;
            dead = true;
            //Destroy(gameObject);
        }
    }

    void LootRoll()
    {
        
        float rnd = Random.Range(0f, 10f);
        Debug.Log("rolled " + rnd);
        if (rnd <= 1.5f)
        {
            Debug.Log("SPAWNED!!!");
            GameObject tmp = Instantiate(loot);
            tmp.transform.position = gameObject.transform.position;
        }
    }

}

public class Enemy
{
    int Health { get; set; }

    public static Transform[] points { get; set; }

    public Enemy(int hp)
    {
        Health = hp;
    }

    public void ReduceHealth(int hp)
    {
        Health -= hp;
    }

    public int GetHealth()
    {
        return Health;
    }

    public void bossHealth(bool ye)
    {
        if (ye)
        {
            Health = Health * 15;
        }
    }
}