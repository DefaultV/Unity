using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvRotation : MonoBehaviour
{

    float lifeTime = 15f;
    // Use this for initialization
    void Start()
    {
        transform.rotation = Random.rotation;
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 1f;
    }

    // Update is called once per frame
    void Update()
    {
        LifeDuration();
        Vector3 dir = new Vector3(transform.position.x, transform.position.y - 20f, transform.position.z) - transform.position;
        transform.Translate(dir.normalized * 20f * Time.deltaTime, Space.World);
    }

    void LifeDuration()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
