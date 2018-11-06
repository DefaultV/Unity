using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileClass : MonoBehaviour {
    SpriteRenderer sr;

    public Sprite Tile_blank;
    public Sprite Tile_dirt;
    public Sprite Tile_water;
    public Sprite Tile_grass;

    public int Height;
    public int type;
    int x;
    int y;
    public GameObject currentCreature;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setCurrentCreature(GameObject c_creature)
    {
        this.currentCreature = c_creature;
    }

    public GameObject getCurrentCreature()
    {
        return this.currentCreature;
    }

    public int GetHeight()
    {
        return Height;
    }
        
    public void SetHeight(int height)
    {
        this.Height = height;
    }

    public void setX(int x)
    {
        this.x = x;
    }
    public void setY(int y)
    {
        this.y = y;
    }
    public int getX()
    {
        return this.x;
    }
    public int getY()
    {
        return this.y;
    }

    public int getType()
    {
        return this.type;
    }

    public void setType(int type)
    {
        switch (type)
        {
            case 1:
                gameObject.GetComponent<SpriteRenderer>().sprite = Tile_blank;
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().sprite = Tile_dirt;
                break;
            case 3:
                gameObject.GetComponent<SpriteRenderer>().sprite = Tile_water;
                break;
            case 4:
                gameObject.GetComponent<SpriteRenderer>().sprite = Tile_grass;
                break;
        }
        this.type = type;
    }
}