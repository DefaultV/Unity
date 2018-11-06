using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Damage : MonoBehaviour {
    float death = 2f;
	// Use this for initialization
	void Start () {
        transform.SetParent(GameObject.Find("Canvas").transform);
	}
	
	// Update is called once per frame
	void Update () {
        death -= Time.deltaTime;
        if (death <= 0)
            Destroy(gameObject);
	}

    public void setPos(Vector3 position)
    {
        var guiPosition = Camera.main.WorldToScreenPoint(position);
        guiPosition.y = Screen.height - guiPosition.y + 40;

        transform.position = guiPosition;
    }

    public void setText(string text)
    {
        GetComponent<Text>().text = text;
    }
}
