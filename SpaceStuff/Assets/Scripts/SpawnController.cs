using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SpawnController : MonoBehaviour {

    //List<GameObject> ESpawns = new List<GameObject>();
    //string path = "Assets/Resources/map1.ss";
    public GameObject enemyPlane;

    public float spawnTimer = 4f;
    // Use this for initialization
    void Start () {
        /*foreach (GameObject spawn in GameObject.FindGameObjectsWithTag("SpawnZone"))
        {
            ESpawns.Add(spawn);
            Debug.Log("Added spawn");
        }*/
        //MapTranslator map = new MapTranslator(path);
	}
	
	// Update is called once per frame
	void Update()
    {
        checkPause();
        if (EndWaves())
            return;
        checkSpawns();
    }

    void checkPause()
    {
        if (EndWaves())
            pauseTime -= Time.deltaTime;

        if (pauseTime <= 0)
        {
            currentWave = 0;
        }
    }

    void checkSpawns()
    {
        if (GameObject.Find("Player").GetComponent<PlaneController>().mainPlayer.GetScore() >= 600)
            return;
        spawnTimer -= Time.deltaTime;
        //Debug.Log(spawnTimer);
        if (spawnTimer <= 0)
        {
            BurstSpawn();
        }
    }

    public float cooldown = 0;
    public bool halt = false;
    public float initCooldown = 0.5f;
    public float duration = 2f;
    public float waveTime = 5f;
    public int waveAmountMax = 3;
    public int currentWave = 0;
    public float pauseTime = 20f;

    void InitTimers()
    {
        cooldown = 0;
        halt = false;
        initCooldown = 0.5f;
        duration = 2f;
        waveTime = 5f;
    }

    bool EndWaves()
    {
        return currentWave >= waveAmountMax;
    }

    public void BurstSpawn()
    {
        duration -= Time.deltaTime;
        if (duration >= 0)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                GameObject ep = Instantiate(enemyPlane);
                ep.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

                cooldown = initCooldown;
            }
        }
        else
            halt = true;

        if (halt)
        {
            spawnTimer = waveTime;
            InitTimers();
            currentWave++;
        }
    }
}


public class MapTranslator
{
    int Enemies { get; set; }
    List<int> Spawn1 { get; set; }
    List<int> Spawn2 { get; set; }
    List<int> Spawn3 { get; set; }
    List<int> Spawn4 { get; set; }
    List<int> Spawn5 { get; set; }


    public MapTranslator(string path)
    {
        StreamReader sr = new StreamReader(path);
        int EnemyCount = 0;
        int change;

        while (!sr.EndOfStream)
        {
            char[] nextline = sr.ReadLine().ToCharArray();
            change = nextline[0];
            EnemyCount += change;
            Enemies += change;

            switch (change)
            {
                case 1:
                    Spawn1.Add(EnemyCount);
                    break;
                case 2:
                    Spawn2.Add(EnemyCount);
                    break;
                case 3:
                    Spawn3.Add(EnemyCount);
                    break;
                case 4:
                    Spawn4.Add(EnemyCount);
                    break;
                case 5:
                    Spawn5.Add(EnemyCount);
                    break;
                default:
                    break;
            }
        }
    }

    public void ExecuteMap(List<GameObject> spawns)
    {
        //spawns[0]
    }
    
    public int EnemiesLeft()
    {
        return Enemies;
    }
}