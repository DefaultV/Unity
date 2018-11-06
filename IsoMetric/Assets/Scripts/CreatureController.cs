using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour {
    int tileX;
    int tileY;
    GameObject currentTile;
    int c_X = 0;
    int c_Y = 0;

    void Start () {

	}
	
	void Update () {
		
	}

    public int getPosX()
    {
        return this.c_X;
    }
    public int getPosY()
    {
        return this.c_Y;
    }

    public void SetCreaturePosition(int x, int y)
    {
        float tileHeight = 0f;
        int oc_X = c_X;
        int oc_Y = c_Y;
        c_X = x;
        c_Y = y;
        if (x < 0)
            c_X = 0;
        if (y < 0)
            c_Y = 0;
        try
        {
            currentTile = GameObject.Find(x.ToString() + "," + y.ToString());
            tileHeight = ((currentTile.GetComponent<tileClass>().Height - 1) * 0.1f);
            if (tileHeight > 0)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = currentTile.GetComponent<SpriteRenderer>().sortingOrder + 51;
            }
            else
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = currentTile.GetComponent<SpriteRenderer>().sortingOrder + 20;
            }
            transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder -1;
            GameObject.Find(oc_X.ToString() + "," + oc_Y.ToString()).GetComponent<tileClass>().setCurrentCreature(null);
            currentTile.GetComponent<tileClass>().setCurrentCreature(gameObject);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            c_X = oc_X;
            c_Y = oc_Y;
            tileHeight = 0f;
        }
        transform.position = new Vector2(currentTile.transform.position.x, currentTile.transform.position.y + tileHeight);
        //Debug.Log("creature is now at: " + x.ToString() + ", " + y.ToString() + "with height: " + tileHeight);
    }

    public GameObject getCurrentTile()
    {
        return this.currentTile;
    }
}
