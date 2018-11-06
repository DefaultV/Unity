using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScroller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Scroll();
	}

    void Scroll()
    {
        gameObject.transform.position = new Vector2(0, gameObject.transform.position.y - 0.1f);
        if (gameObject.transform.position.y <= -60f)
        {
            gameObject.transform.position = new Vector2(0,119f);
        }
    }
}
