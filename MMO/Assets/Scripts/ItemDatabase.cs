using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
	private List<Item> database = new List<Item>();
	private JsonData itemData;

	void Start(){
        JsonMapper.RegisterExporter<float>((obj, writer) => writer.Write(System.Convert.ToDouble(obj)));
        JsonMapper.RegisterImporter<double, float>(input => System.Convert.ToSingle(input));
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
		ConstructItemDatabase ();

		//Debug.Log (database [1].Slug);
		//Debug.Log (database [1].Attackspeed.ToString("F1"));
        //Debug.Log(database[1].maxAttack);
        //Debug.Log (database [0].Intellect);
	}

	void ConstructItemDatabase(){
		for (int i = 0; i<itemData.Count; i++){
			database.Add (new Item ((int)itemData[i]["id"], itemData[i]["title"].ToString (), (int)itemData[i]["value"],
				(int)itemData[i]["stats"]["strength"],(int)itemData[i]["stats"]["intellect"],
                (int)itemData[i]["stats"]["minattack"], (int)itemData[i]["stats"]["maxattack"], (int)itemData[i]["stats"]["attackspeedF"], (int)itemData[i]["stats"]["attackspeedS"],
                (int)itemData[i]["requirements"], itemData[i]["description"].ToString(), (bool)itemData[i]["stackable"],(int)itemData[i]["rarity"],
				itemData[i]["slug"].ToString()
				));
		}
	}

    public Item FetchItemByID(int id)
    {
        for (int i = 0; i<database.Count; i++)
        {
            if (database[i].ID == id)
                return database[i];
        }
        return null;
    }

}

public class Item{
	public int ID { get; set;}
	public string Title { get; set;}
	public int Value { get; set;}

	public int Strength { get; set;}
	public int Intellect {get;set;}
	public int MinAttack{ get; set;}
    public int MaxAttack { get; set; }
    //public float AttackspeedF { get; set;}
    //public float AttackspeedS { get; set; }
    public float Attackspeed { get; set; }

    public int Requirements { get; set;}
	public string Description{ get; set;}
	public bool Stackable{ get; set;}
	public int Rarity{ get; set;}
	public string Slug{ get; set;}
    public Sprite Sprite { get; set; }

	public Item(int id, string title, int value){
		this.ID = id;
		this.Title = title;
		this.Value = value;
	}
	
	public Item(int id, string title, int value, int strength, int intellect, int requirements, string description, bool stackable, int rarity, string slug){
		this.ID = id;
		this.Title = title;
		this.Value = value;
		this.Strength = strength;
		this.Intellect = intellect;
		this.Requirements = requirements;
		this.Description = description;
		this.Stackable = stackable;
		this.Rarity = rarity;
		this.Slug = slug;
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
	}

	public Item(int id, string title, int value, int strength, int intellect, int minattack, int maxattack, int attackspeedF, int attackspeedS, int requirements, string description, bool stackable, int rarity, string slug){
		this.ID = id;
		this.Title = title;
		this.Value = value;
		this.Strength = strength;
		this.Intellect = intellect;
		this.MinAttack = minattack;
        this.MaxAttack = maxattack;
        if (attackspeedF == 0 || attackspeedS == 0)
            this.Attackspeed = 0;
        else
            this.Attackspeed = attackspeedF / attackspeedS;
        this.Requirements = requirements;
		this.Description = description;
		this.Stackable = stackable;
		this.Rarity = rarity;
		this.Slug = slug;
	}

	public Item(){
		this.ID = -1;
	}
}
