using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagScript : MonoBehaviour {
    public GameObject objectToAttach;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Camera.main.WorldToScreenPoint(objectToAttach.transform.position + Vector3.up * 0.5f);


        if (transform.position.z < 0)
            GetComponent<Text>().text = "";
        else if (Vector3.Distance(objectToAttach.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= 5f)
        {
            GetComponent<Text>().text = objectToAttach.tag.ToString();  
        }
        else
            GetComponent<Text>().text = "";
    }
}
