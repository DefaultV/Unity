using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBossScript : MonoBehaviour {
    public int health;
    public Enemy meAsEnemy;
    //GameObject player;
    float distanceToPlayer;

    private Transform[] points;
    private float speed = 4f;

    public Transform waypointTarget;
    private int wavepointIndex = 0;

    public float countdownToShoot = -4f;
    public float lazerCooldown = 8f;
    public AudioClip lazersound;
    public AudioClip bossexplosion;

    public GameObject particlePart;

    public GameObject loot;

    public GameObject Gun1;
    public GameObject Gun2;
    public GameObject BossRocket;

    GameObject Player;

    public Material start;
    public Material hurt;

    bool canMove = false;
    bool bossArrived = false;

    // Use this for initialization
    void Start () {
        Gun1.GetComponent<LineRenderer>().enabled = false;
        meAsEnemy = new Enemy(health);
        meAsEnemy.bossHealth(true);

        Player = GameObject.Find("Player");

        points = new Transform[GameObject.Find("Waypoints" + gameObject.tag).transform.childCount];
        Debug.Log(points);
        Debug.Log(points.Length);

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = GameObject.Find("Waypoints" + gameObject.tag).transform.GetChild(i);

        }
        waypointTarget = points[0];
    }
	
	// Update is called once per frame
	void Update () {
        GetComponentInChildren<MeshRenderer>().materials[0].color = Color.white;
        if (!(GameObject.Find("Player").GetComponent<PlaneController>().mainPlayer.GetScore() >= 600))
            return;


        
        CheckHealthOfMe();
        if (dead)
            return;
        Shoot();
        ShootRockets();
        CheckPlayerPos();
        //waypoint
        Vector3 dir = waypointTarget.position - gameObject.transform.position;
        if (canMove)
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, waypointTarget.position) <= 0.2f)
        {
            bossArrived = true;
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        wavepointIndex++;
        if (wavepointIndex > points.Length-1)
            wavepointIndex = 0;
        
        waypointTarget = points[wavepointIndex];
    }

    void CheckPlayerPos()
    {
        if (Player.transform.position.x >= gameObject.transform.position.x -1.5f && Player.transform.position.x <= gameObject.transform.position.x + 1.5f && lazerCooldown <= 0f)
        {
            countdownToShoot = 2.4f;
            lazerCooldown = 11f;
        }
    }
    Transform[] r_point;
    int rocketCount = 0;
    float rocketCooldown = 5f;

    void ShootRockets()
    {
        rocketCooldown -= Time.deltaTime;
        if (rocketCooldown > 0)
            return;

        if (rocketCount == 2)
            rocketCount = 0;
        r_point = new Transform[Gun2.transform.childCount];
        for (int i = 0; i < r_point.Length; i++)
        {
            r_point[i] = Gun2.transform.GetChild(i);
        }
        GameObject tmp = Instantiate(BossRocket);
        tmp.transform.position = r_point[rocketCount].position;
        rocketCount++;
        rocketCooldown = 5f;
    }

    void Shoot()
    {
        countdownToShoot -= Time.deltaTime;
        if (countdownToShoot <= 2.3f && countdownToShoot > 0f)
        {
            canMove = false;
            Gun1.GetComponent<ParticleSystem>().Play();
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().PlayOneShot(lazersound, 0.4f);
        }
        if (countdownToShoot <= 0 && countdownToShoot >= -5f)
        {
            castTriggers = true;
            //GetComponent<AudioSource>().PlayOneShot(lazersound);
            //countdownToShoot = Random.Range(2f, 7f);
            //lazer
            Gun1.GetComponent<LineRenderer>().enabled = true;
            Gun1.GetComponent<LineRenderer>().SetPosition(1, newX());
            RayCastLine();
            Gun1.GetComponent<ParticleSystem>().Stop();

        }
        if (countdownToShoot <= -5f)
        {
            lazerCooldown -= Time.deltaTime;
            canMove = true;
            castTriggers = false;
            sway = 0;
            Gun1.GetComponent<LineRenderer>().enabled = false;
        }
    }

    float sway = 0;
    public bool castTriggers = false;
    bool swayDir = false;
    Vector3 newX()
    {
        if (!castTriggers)
            return new Vector3(0,0,0);
        if (swayDir)
            sway += Time.deltaTime*4;
        else
            sway -= Time.deltaTime*4;

        if (sway >= 2f)
            swayDir = false;
        else if (sway <= -2f)
            swayDir = true;
        
        return new Vector3(sway, -4, 0);
    }

    RaycastHit hit;
    void RayCastLine()
    {
        Debug.DrawRay(new Vector3(transform.position.x,-1f,0), new Vector3(sway*2f, -4, 0));
        if (Physics.Raycast(new Vector3(transform.position.x, -1f, 0), new Vector3(sway*2f, -4, 0), out hit))
        {
            if (hit.transform.name == "Player")
            {
                Debug.Log("Hit PLAYER!" + hit.transform.gameObject.name + "bool: " + castTriggers);
                hit.transform.gameObject.GetComponent<PlaneController>().TakeDamage();
            }
        }
    }

    void OnParticleCollision(GameObject particleHolderObject)
    {
        if (particleHolderObject.tag == "Enemy")
            return;
        if (particleHolderObject.name == "PowerUpBeamDeflector")
        {
            if (Random.Range(0, 2) == 1)
                meAsEnemy.ReduceHealth(200);
        }
        meAsEnemy.ReduceHealth(1);
        if (Random.Range(0, 2) == 1)
            GetComponentInChildren<MeshRenderer>().materials[0].color = Color.red;
        //GetComponentInChildren<MeshRenderer>().material = start;
    }

    void DisableMe()
    {
        LootRoll();
        GameObject.Find("Environment").GetComponent<UIBehaviour>().countdown = true;
        GetComponentInChildren<BoxCollider>().enabled = false;
        gameObject.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<ParticleSystem>().Stop();
        gameObject.GetComponent<BoxCollider>().enabled = false;
        GetComponentInChildren<MeshRenderer>().enabled = false;
        GetComponentInChildren<Light>().enabled = false;
        foreach (GameObject light in GameObject.FindObjectsOfType<GameObject>())
        {
            if (light.tag == "bossLight")
                light.GetComponent<Light>().enabled = false;
        }
        //gameObject.GetComponent<LazerBossScript>().enabled = false;
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

            GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 0.2f;
            GetComponent<AudioSource>().PlayOneShot(bossexplosion, 5);
            GameObject.Find("BossDeath").GetComponent<ParticleSystem>().Play();
            GameObject.Find("Flash").GetComponent<BossFlash>().begin = true;

            countdownToShoot = 100f;
            dead = true;
        }
    }

    bool lootOnce = false;
    void LootRoll()
    {
        if (lootOnce)
            return;
        float rnd = Random.Range(0f, 4f);
        Debug.Log("rolled " + rnd);
        if (rnd >= 3.5f)
        {
            Debug.Log("SPAWNED!!!");
            GameObject tmp = Instantiate(loot);
            tmp.transform.position = gameObject.transform.position;
        }
        lootOnce = true;
    }
}
