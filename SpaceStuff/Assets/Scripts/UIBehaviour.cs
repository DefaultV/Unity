using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBehaviour : MonoBehaviour {
    public bool countdown = false;
    public float countdownScore = 5f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!countdown)
            return;
        countdownScore -= Time.deltaTime;

        if (countdownScore <= 2f && countdownScore > 0f)
            GameObject.Find("Flash").GetComponent<BossFlash>().beginOutro = true;

        if (countdownScore <= 0f)
        {
            GameObject.Find("SaveState").GetComponent<SaveState>().Complete();
            UnityEngine.SceneManagement.SceneManager.LoadScene("ActSelection");
        }
	}
}
