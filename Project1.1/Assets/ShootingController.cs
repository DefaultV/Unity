using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShootingController : NetworkBehaviour
{
    public GameObject Bullet;
    public AudioSource aSource;
    public AudioClip onFire;
    Transform Barrel;
    Transform Player2D;
    Camera newCam;

    public KeyCode Shoot;
    

    //cooldown shoot
    float Cooldown = 0.0f;
    float power = 200.1f;

    [Command]
    void CmdSpawnBullet()
    {
        Barrel = transform.FindChild("Barrel");
        GameObject temp_bullet = Instantiate(Bullet, Barrel.transform.position, Quaternion.identity);
        temp_bullet.GetComponent<Rigidbody2D>().AddForce(Barrel.up * power);


        NetworkServer.Spawn(temp_bullet);

    }

    void Reset()
    {
        Barrel = transform.FindChild("Barrel");
    }
    // Use this for initialization
    void Start () {
        Player2D = GetComponent<Transform>();
        aSource = GetComponent<AudioSource>();
        Barrel = transform.FindChild("Barrel");
    }
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;
        if (Cooldown > 0.0f)
            Cooldown -= 0.2f;

        if (Input.GetKey(Shoot) && Cooldown <= 0.0f)
        {
            aSource.PlayOneShot(onFire);
            CmdSpawnBullet();
            Cooldown = 50.0f;
        }
    }
}
