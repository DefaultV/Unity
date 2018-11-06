using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class ItemDatabase : MonoBehaviour {

    public List<Item> database;
    JsonData itemData;

	// Use this for initialization
	void Start () {
        database = new List<Item>();
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));

        ContructDatabase();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ContructDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            database.Add(new Item((int)itemData[i]["id"], itemData[i]["name"].ToString(), (int)itemData[i]["value"]));
        }
    }
}


public class Item
{
    int ID { get; set; }
    string Name { get; set; }
    int Value { get; set; }

    public Item()
    {
        ID = -1;
    }

    public Item(int id, string name, int value)
    {
        ID = id;
        Name = name;
        Value = value;
    }
}

public class Utility : Item
{
    int Strength { get; set; }

    public Utility()
    {

    }
}

public class Armor : Item
{
    int Defense { get; set; }

    public Armor()
    {

    }
}

public class Weapon : Item
{
    int Damage { get; set; }

    public Weapon()
    {

    }
}