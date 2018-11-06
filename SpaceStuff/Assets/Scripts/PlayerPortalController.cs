using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortalController : MonoBehaviour {

    float destroyTimer = 4f;
    public AudioClip portalStart;
    public AudioClip portalLoop;
    // Use this for initialization
    void Start () {
        GetComponent<AudioSource>().PlayOneShot(portalStart);
	}
	
	// Update is called once per frame
	void Update () {
        if (!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().PlayOneShot(portalLoop);
        destroyTimer -= Time.deltaTime;
        if (destroyTimer <= 0f)
        {
            for (int i = 0; i < GameObject.Find("PlayerPortal").transform.childCount; i++)
            {
                GameObject.Find("PlayerPortal").transform.GetChild(i).GetComponent<ParticleSystem>().Stop();
            }
        }
        if (destroyTimer <= -1f)
            Destroy(gameObject);
	}
}
