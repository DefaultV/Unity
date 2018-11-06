using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


public class Player
{
    public int Health { get; set; }
    public int Attack { get; set; }

    public Player()
    {
        Health = -1;
    }

    public Player(int hp, int atk)
    {
        Health = hp;
        Attack = atk;
    }
}
