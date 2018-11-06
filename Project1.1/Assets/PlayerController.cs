using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

    // Use this for initialization
    private Rigidbody2D rb2D; //the Rigidbody inside our object 
    public Transform trans2D;
    public CircleCollider2D CC2D;
    public CircleCollider2D CCE2D;
    //camera

    public Camera ClientCam;
    //audio
    public AudioClip onFire;
    public AudioSource source;
    public void Start()
    {

        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }
        else
        {
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }

        //new camera for client
        /*
        newCam = Instantiate(ClientCam, transform.position, Quaternion.identity);
        newCam.enabled = true;
        newCam.GetComponent<RectTransform>().localPosition = new Vector3(transform.position.x, transform.position.y, -2);
        NetworkServer.Spawn(newCam.GetComponent<GameObject>());
        Camera.main.enabled = false;*/
    }
    public void Awake()
    {

        rb2D = GetComponent<Rigidbody2D>();
        trans2D = gameObject.GetComponent<Transform>();
        source = GetComponent<AudioSource>();
        CC2D = gameObject.GetComponent<CircleCollider2D>();


        //ClientScene.RegisterPrefab(Bullet);
    }
    public KeyCode Right;
    public KeyCode Left;
    public KeyCode Up;
    public KeyCode Down;
    public KeyCode Shoot;


    public float X = 0.0f;
    public float Y = 0.0f;
    public float BulletVelocity = 2.0f;
    [SyncVar]
    public float Health = 50;

    Vector2 mousePosv2;

    //Bullet
    //public GameObject Bullet;
    //Vector2 BulletPos;
    //Vector2 temp_bulletpos;
    public bool touch;



    //collision
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider)
        {
            touch = true;
        }
    }

    //respawn
    public GameObject Player;
    // Update is called once per frame

    void Update()
    {
        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }
        else
        {
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }


        //collision
        if (touch)
        {
            Health = Health - 25;
            touch = false;
        }
        if (Health <= 0)
        {
            RpcRespawn();
            Health = 50;
        }

        //CONTROLS
        if (Input.GetKey(Right))
        {
            X = 3.0f;
        }
        else if (!Input.GetKey(Left))
            X = 0;
        if (Input.GetKey(Left))
            X = -3.0f;
        else if (!Input.GetKey(Right))
            X = 0;

        if (Input.GetKey(Up))
            Y = 3.0f;
        else if (!Input.GetKey(Down))
            Y = 0;
        if (Input.GetKey(Down))
            Y = -3.0f;
        else if (!Input.GetKey(Up))
            Y = 0;

        if (Input.GetKey(Right) && Input.GetKey(Left) && Input.GetKey(Up) && Input.GetKey(Down))
        {
            X = 0.0f;
            Y = 0.0f;
        }
        if (Input.GetKey(Right) && Input.GetKey(Left))
        {
            X = 0.0f;
        }
        if (Input.GetKey(Up) && Input.GetKey(Down))
        {
            Y = 0.0f;
        }
        //END CONTROLS

        //PLAYER FOLLOW MOUSE
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        trans2D.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - trans2D.position);
        //END PLAYER FOLLOW MOUSE



        //UPDATE CHAR
        rb2D.velocity = new Vector2(X, Y);
        //END UPDATE CHAR

    }
    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            transform.position = new Vector2(0, 0);
        }
    }
}
