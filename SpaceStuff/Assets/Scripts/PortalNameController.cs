using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalNameController : MonoBehaviour {
    GameObject saveState;
	// Use this for initialization
	void Start () {
        saveState = GameObject.Find("SaveState");
        Debug.Log(saveState.GetComponent<SaveState>().FetchDataFromAct(1).GetScore());
        if (saveState.GetComponent<SaveState>().FetchDataFromAct(1).GetScore() > 0)
        {
            GameObject.Find("Act1Text").GetComponent<Text>().text = string.Format("<color=#00C0FFFF>Act 1\nScore: {0}</color>",saveState.GetComponent<SaveState>().FetchDataFromAct(1).GetScore());
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
