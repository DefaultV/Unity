using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDatabase : MonoBehaviour {
    List<Item> itemDatabase;
    List<Spell> spellDatabase;

	// Use this for initialization
	void Start () {
        itemDatabase = GetComponent<ItemDatabase>().database;
        spellDatabase = GetComponent<SpellDatabase>().database;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}


public class MUnit
{
    int Health { get; set; }
    int Resource { get; set; }


    MUnit()
    {

    }
}