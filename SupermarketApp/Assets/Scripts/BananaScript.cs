using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaScript : MonoBehaviour {

    GameObject Ply;
    public GameObject checkmarkOn;
    // Use this for initialization
    void Start () {
        Ply = GameObject.FindGameObjectWithTag("Player");

        checkmarkOn.SetActive(false);
    }

    // Update is called once per frame
    bool disCheck = false;
    void Update()
    {
        if (Vector3.Distance(Ply.transform.position, transform.position) <= 5f)
        {
            if (disCheck)
                return;

            transform.Translate((new Vector3(1, 0.2f, 0) - transform.localPosition).normalized * 2f * Time.deltaTime);
            if (Vector3.Distance(transform.localPosition, new Vector3(1,0.2f,0)) <= 0.1f)
            {
                GetComponentInChildren<Light>().enabled = true;
                disCheck = !disCheck;
            }

            checkmarkOn.SetActive(true);
        }
        else
        {
            transform.Translate((new Vector3(0, 0.2f, 0) - transform.localPosition).normalized * 2f * Time.deltaTime);
            GetComponentInChildren<Light>().enabled = false;
            disCheck = false;
        }
    }
}
