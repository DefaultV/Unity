using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public Player m_player;
    private float drawRangeUI = 5.0f;
    public GameObject nametag;
    private GameObject NamePanel;
    // Use this for initialization
    void Start () {
        Player newPly = new Player(0, 0);
        m_player = newPly;
        newPly.Name = "Defualt";

        //init tag
        GameObject newTag = Instantiate(nametag);
        newTag.GetComponent<nameTags>().Player = this.gameObject;
        NamePanel = GameObject.Find("NamePanel");
        newTag.GetComponent<Transform>().SetParent(NamePanel.transform);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

public class Player
{
    public string Race { get; set; }
    public string Char_Class { get; set; }

    public int Level { get; set; }
    public int Health { get; set; }
    public int Resource { get; set; }
    public int Armor { get; set; }
    public int Resistance { get; set; }
    public int Conduit { get; set; }
    public float Attackspeed { get; set; }

    public string Name  { get; set; }
    public string Title { get; set; }
    public string Association { get; set; }

    public Player()
    {
        Name = "Null-Player";
        Health = 1;
    }

    public Player(int race, int char_class)
    {
        Health = 0;
        Resource = 0;
        Armor = 0;
        Resistance = 0;
        Attackspeed = 0f;
        Level = 1;

        switch (race)
        {
            case 0:
                Race = "Human";
                Health += 50;
                Resource += 70;
                Armor += 2;
                Resistance += 0;
                Attackspeed += 1.5f;
                break;
            case 1:
                Race = "Beast";
                Health += 80;
                Resource += 30;
                Armor += 4;
                Resistance += 2;
                Attackspeed += 1.0f;
                break;
            case 2:
                Race = "Elemental";
                Race = "Beast";
                Health += 60;
                Resource += 60;
                Armor += 0;
                Resistance += 8;
                Attackspeed += 1.0f;
                break;
        }

        switch (char_class)
        {
            case 0:
                if (race == 0 || race == 1) //Only human and beast can be knights
                    Char_Class = "Knight";
                else
                    Char_Class = "Spellslinger";
                break;
            case 1:
                if (race == 0 || race == 2) //Only human and elemental can be spellslingers
                    Char_Class = "Spellslinger";
                else
                    Char_Class = "Knight";
                break;
        }

        Name = "Unknown";
        Title = "Unknown";
    }
}