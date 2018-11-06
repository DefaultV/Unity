using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneController : MonoBehaviour {

    GameObject PlayerPlane;
    public Player mainPlayer;
    public GameObject Bullet;
    public GameObject lifeBlock;
    public GameObject SuperPowerParticles;
    public AudioClip gatlingstart;
    public AudioClip gatlingloop;
    public AudioClip gatlingend;
    public AudioClip rocketlaunch;
    public AudioClip hulldamagesound;
    public AudioClip superpower;

    List<GameObject> GatlinGuns = new List<GameObject>();
    List<GameObject> GatlinLights = new List<GameObject>();

    float Timer;
    float Invincibility = 0f;

    bool CanShoot = true;

    bool isShooting = false;
    bool ShotStarted = false;
    bool ShotEnded = true;

    int muzzleCount = 0;
    float timerForMuzzle = 0.4f;

    public float cooldownGatlins = 0f;

    public bool joystickEnable = true;

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        PlayerPlane = gameObject;
        mainPlayer = new Player();
        mainPlayer.SetRockets(10);
        Timer = 0f;
        foreach(GameObject gun in GameObject.FindGameObjectsWithTag("GatPart"))
        {
            GatlinGuns.Add(gun);
        }
        foreach(GameObject light in GameObject.FindGameObjectsWithTag("GatLight"))
        {
            GatlinLights.Add(light);
        }
        StopGatlins();
        //INTRO SEQUENCE
        gameObject.transform.position = new Vector3(0, -8f, 3);
        dir = new Vector3(0,-10f, -1) - gameObject.transform.position;
        EnableDisablePlayer(false);
        //
    }

    public void EnableDisablePlayer(bool y)
    {
        if (!y)
        {
            GameObject.Find("Shield").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Plane").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Cylinder_001").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Cylinder_002").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Flame").GetComponent<ParticleSystem>().Stop();
        }
        else
        {
            GameObject.Find("Plane").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Cylinder_001").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Cylinder_002").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Flame").GetComponent<ParticleSystem>().Play();
        }
        
    }

    public float introSequence = 3f;
    Vector3 dir;

    public bool plyRotateParent = false;

    void IntroSequenceAnimation()
    {
        transform.Translate(dir.normalized * 2f * Time.deltaTime, Space.World);
    }

    void Update () {
        introSequence -= Time.deltaTime;
        //intro
        if (introSequence > 2f)
            return;
        if (introSequence > 0 && introSequence < 2f)
        {
            EnableDisablePlayer(true);
            IntroSequenceAnimation();
            return;
        }
        else
            introSequence = -1f;

        //////
        GameObject.Find("Shield").GetComponent<MeshRenderer>().enabled = true;
        if (!plyRotateParent && !joystickEnable)
            UpdatePos();
        else if (plyRotateParent && !joystickEnable)
            UpdateRot();
        else if (joystickEnable)
            JoyMove();

        Cooldown();
        if (Input.GetKeyDown(KeyCode.Mouse1) && CanShoot && mainPlayer.GetRockets() > 0 || Input.GetAxis("JoyFire2") > 0 && CanShoot && mainPlayer.GetRockets() > 0)
        {
            Shoot();
            mainPlayer.SetRockets(mainPlayer.GetRockets() - 1);
        }
        if (Input.GetKey(KeyCode.Mouse0) && cooldownGatlins <= 0f || Input.GetAxis("JoyFire1") > 0 && cooldownGatlins <= 0f)
        {
            StartGatlins();
            isShooting = true;
            checkShooting();
        }
        else
        {
            StopGatlins();
            isShooting = false;
            checkShooting();
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            SuperPowerParticles.GetComponent<ParticleSystem>().Play();
            SuperPowerParticles.GetComponent<AudioSource>().PlayOneShot(superpower);
        }

        //Invic
        Invincibility -= Time.deltaTime;
        if (Invincibility >= 0f)
        {
            GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("Shield").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Shield").transform.Rotate(new Vector3(GameObject.Find("Shield").transform.rotation.x + 2, GameObject.Find("Shield").transform.rotation.y + 4, 0));
            //GameObject.Find("Shield").GetComponent<MeshRenderer>().materials[0].color = Color.blue;
            //Debug.Log(GameObject.Find("Shield").GetComponent<MeshRenderer>().materials[0].color);
        }
        else
        {
            GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Shield").GetComponent<MeshRenderer>().enabled = false;
        }
    }

    //Collision
    void OnParticleCollision(GameObject particleHolderObject)
    {
        Debug.Log(particleHolderObject.name);
        if (Invincibility >= 0f || particleHolderObject.tag == "GatPart" || particleHolderObject.tag == "Super")
            return;
        //if (particleHolderObject.name == "ParticleGun")
        //{
        TakeDamage();
        //}
    }

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log(coll.gameObject.name);
        if (coll.tag == "Enemy")
            TakeDamage();
    }

    public void TakeDamage()
    {
        if (Invincibility >= 0f)
            return;
        mainPlayer.TakeDamage(1);
        LifeBlock(true);
        GameObject.FindGameObjectWithTag("AudioRocket").GetComponent<AudioSource>().PlayOneShot(hulldamagesound);
        Invincibility = 5f;
    }

    void LifeBlock(bool loose)
    {
        if (!loose)
        {
            GameObject lb = Instantiate(lifeBlock);
            lb.transform.SetParent(GameObject.Find("Lives").transform);
            lb.transform.position = Vector2.zero;
        }
        else
        {
            if (GameObject.Find("Lives").transform.childCount >= 2)
            {
                Debug.Log("ccount" + GameObject.Find("Lives").transform.childCount);
                Destroy(GameObject.Find("Lives").transform.GetChild(0).gameObject);
                //DEAD IF HERE
            }
            else
            {
                GameObject.Find("SaveState").GetComponent<SaveState>().Complete();
                UnityEngine.SceneManagement.SceneManager.LoadScene("ActSelection");
            }
        }
    }
    /// <summary>
    /// CUSTOM
    /// </summary>
    /// 

    void checkShooting()
    {
        if (isShooting)
        {
            timerForMuzzle -= Time.deltaTime;
            if (timerForMuzzle <= 0)
                muzzleCount++;
            if (!ShotStarted)
            {
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().PlayOneShot(gatlingstart);
                ShotStarted = true;
                //Debug.Log("First started");
                ShotEnded = false;
            }
            else if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().PlayOneShot(gatlingloop);
                GetComponent<AudioSource>().loop = true;
                //Debug.Log("Loop started");
            }
            //FLASHING LIGHTS FROM GATLINS
            if (muzzleCount >= 5)
            {
                foreach (GameObject light in GatlinLights)
                {
                    if (light.GetComponent<Light>().enabled)
                    {
                        light.GetComponent<Light>().enabled = false;
                        muzzleCount = 0;
                    }
                    else
                    {
                        light.GetComponent<Light>().enabled = true;
                        muzzleCount = 0;
                    }
                }
            }

        }
        else if (!isShooting && !ShotEnded)
        {
            GetComponent<AudioSource>().loop = false;
            ShotStarted = false;
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(gatlingend);
            ShotEnded = true;
            cooldownGatlins = 0.5f;
            timerForMuzzle = 0.4f;
            //Debug.Log("Loop Ended");
        }
    }

    void StartGatlins(){
        Camera.main.GetComponent<CameraShake>().sShake = true;
        GetComponentInChildren<Animator>().SetBool("TriggerRollAnim", true);
        foreach (GameObject gun in GatlinGuns)
        {
            if (gun.GetComponent<ParticleSystem>().isPlaying)
                return;
            gun.GetComponent<ParticleSystem>().Play();
        }
    }

    void StopGatlins()
    {
        cooldownGatlins -= Time.deltaTime;
        Camera.main.GetComponent<CameraShake>().sShake = false;
        GetComponentInChildren<Animator>().SetBool("TriggerRollAnim", false);
        foreach (GameObject gun in GatlinGuns)
        {
            gun.GetComponent<ParticleSystem>().Stop();
        }
        //lights
        foreach (GameObject light in GatlinLights)
        {
            light.GetComponent<Light>().enabled = false;
        }
    }

    void UpdatePos()
    {
        Vector3 mouspos = Input.mousePosition;
        mouspos.z = 15f;
        Vector3 wantedPos = Camera.main.ScreenToWorldPoint(mouspos);
        wantedPos.y = wantedPos.y / 1.5f;
        //Debug.Log(wantedPos.y);
        if (wantedPos.y <= -3f)
            wantedPos.y = -3f;

        PlayerPlane.transform.position = new Vector3(wantedPos.x, wantedPos.y-10f,0);
        Debug.DrawRay(Camera.main.transform.position, wantedPos);
    }

    float j_speed = 5f;
    Vector3 joy_oldpos = Vector3.zero;
    void JoyMove()
    {
        float hori = Input.GetAxis("JoyHorizontal");
        float vert = -Input.GetAxis("JoyVertical");
        joy_oldpos = transform.position;
        if (Input.GetAxis("JoyHorizontal") < 0.4 && Input.GetAxis("JoyHorizontal") > -0.4)
            hori = 0f;
        if (Input.GetAxis("JoyVertical") > -0.4 && Input.GetAxis("JoyVertical") < 0.4)
            vert = 0f;

        Vector3 movement = new Vector3(hori * j_speed, vert * j_speed, 0);
        GetComponent<CharacterController>().Move(movement * Time.deltaTime);
        if (transform.position.x >= 20 || transform.position.y >= -5)
            transform.position = joy_oldpos;
        if (transform.position.x <= -20 || transform.position.y <= -12.5)
            transform.position = joy_oldpos;
    }

    void UpdateRot()
    {
        //Cursor.lockState = CursorLockMode.Confined;
        gameObject.transform.localPosition = new Vector3(0, -8, -6);
        Vector3 mouspos = Input.mousePosition;
        mouspos.z = -4f;
        mouspos.y = 0f;
        gameObject.transform.parent.eulerAngles = new Vector3(0,-mouspos.x/4f,0);
    }

    void Shoot()
    {
        GameObject tempBullet = Instantiate(Bullet);
        tempBullet.transform.position = new Vector3(PlayerPlane.transform.position.x, -6.3f);
        CanShoot = false;
        Timer = 1f;
        GameObject.FindGameObjectWithTag("AudioRocket").GetComponent<AudioSource>().PlayOneShot(rocketlaunch);
    }

    void Cooldown()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            CanShoot = true;
        }
    }
}

public class Player
{
    int Health { get; set; }
    int Lazer { get; set; }
    int Rockets { get; set; }
    int Score { get; set; }

    public Player()
    {
        Health = 3;
        Lazer = 0;
        Score = 0;
    }

    public Player(int hp, int pu)
    {
        Health = hp;
        Lazer = pu;
        Score = 0;
    }

    public void TakeDamage(int dmg)
    {
        Health -= dmg;
    }

    public int GetScore()
    {
        return Score;
    }

    public int GetRockets()
    {
        return Rockets;
    }

    public void SetRockets(int val)
    {
        Rockets = val;
        GameObject.Find("RocketsAmount").GetComponent<Text>().text = Rockets.ToString();
    }

    public int GetLazer()
    {
        return Lazer;
    }

    public void SetLazer(int val)
    {
        Lazer = val;
        GameObject.Find("LazerAmount").GetComponent<Text>().text = Lazer.ToString();
    }

    public void UpdateScore(int val)
    {
        Score += val;
        GameObject.Find("Score").GetComponent<Text>().text = Score.ToString();
        Debug.Log("SCORE = " + Score);
    }
}