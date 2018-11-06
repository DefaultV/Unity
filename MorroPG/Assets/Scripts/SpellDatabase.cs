using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDatabase : MonoBehaviour {

    public List<Spell> database;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


public class Spell
{
    int Damage { get; set; }
    int Range { get; set; }

    public Spell()
    {

    }
}