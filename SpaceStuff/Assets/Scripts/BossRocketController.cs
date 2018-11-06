using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRocketController : MonoBehaviour {

    GameObject Player;
    float speed = 15f;

    bool activateRocket = false;
    public float countDown = 2f;
    float scaleInc = 0f;
    Vector3 dir;
    Vector3 oldPlayerPos;
    // Use this for initialization
    void Start () {
        Player = GameObject.Find("Player");
        GetComponent<SphereCollider>().enabled = false;
        transform.localScale = new Vector3(scaleInc, scaleInc, scaleInc);
    }
	
	// Update is called once per frame
	void Update () {
        if (activateRocket)
            RocketGo();
        else
            RocketIdle();

        countDown -= Time.deltaTime;
        if (countDown <= 0f)
        {
            activateRocket = true;
        }
        scaleInc += Time.deltaTime / 10f;
        if (scaleInc >= 0.2f)
            return;
        transform.localScale = new Vector3(scaleInc, scaleInc, scaleInc);
    }

    void RocketIdle()
    {
        oldPlayerPos = Player.transform.position;
        oldPlayerPos.z = -1f;
        gameObject.transform.LookAt(oldPlayerPos);
    }

    float dead = 0.3f;
    void RocketGo()
    {
        dir = oldPlayerPos - gameObject.transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(oldPlayerPos, gameObject.transform.position) <= 0.2f)
        {
            if (Vector3.Distance(Player.transform.position, gameObject.transform.position) <= 5f)
            {
                Player.GetComponent<PlaneController>().TakeDamage();
            }
            dead -= Time.deltaTime;
            if (dead <= 0)
                Destroy(gameObject);
            GetComponent<SphereCollider>().enabled = true;
            if (!GetComponent<ParticleSystem>().isPlaying)
                GetComponent<ParticleSystem>().Play();
            GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }
}
