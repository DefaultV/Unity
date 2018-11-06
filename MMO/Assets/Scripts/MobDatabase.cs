using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public class MobDatabase : MonoBehaviour {
    private List<Mob> database = new List<Mob>();
    private JsonData MobData;

    void Start()
    {
        JsonMapper.RegisterExporter<float>((obj, writer) => writer.Write(System.Convert.ToDouble(obj)));
        JsonMapper.RegisterImporter<double, float>(input => System.Convert.ToSingle(input));
        MobData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Mobs.json"));
        ConstructMobDatabase();

        //Debug.Log (database [1].Attackspeed.ToString("F1"));
        //Debug.Log(database[1].maxAttack);
        //Debug.Log (database [0].Intellect);
    }

    void ConstructMobDatabase()
    {
        for (int i = 0; i < MobData.Count; i++)
        {
            database.Add(new Mob(
                (int)MobData[i]["id"], (int)MobData[i]["health"], (int)MobData[i]["resource"], (int)MobData[i]["alignment"],
                (int)MobData[i]["armor"], (int)MobData[i]["resistance"], (int)MobData[i]["conduit"], 
                (int)MobData[i]["minap"], (int)MobData[i]["maxap"], (int)MobData[i]["m_minas"], (int)MobData[i]["m_maxas"],
                MobData[i]["name"].ToString(), MobData[i]["title"].ToString(), MobData[i]["association"].ToString()
                ));
        }
    }

    public Mob FetchMobByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].ID == id)
                return database[i];
        }
        return null;
    }
}

public class Mob
{
    public int ID { get; set; }
    public int Health { get; set; }
    public int Resource { get; set; }
    public int Alignment { get; set; }
    public int Armor { get; set; }
    public int Resistance { get; set; }
    public int Conduit { get; set; }
    public float AggroRange { get; set; }

    public int Minap { get; set; }
    public int Maxap { get; set; }
    public float M_Attackspeed { get; set; }

    //Elemental edgyness
    //Conduit item gives different spells depending on slot


    public string Name { get; set; }
    public string Title { get; set; }
    public string Association { get; set; }

    public Mob(int id, int health, int resource, int alignment, int armor, int resistance, int conduit, int minap, int maxap, int m_minas, int m_maxas ,string name, string title, string association)
    {
        this.ID = id;
        Health = health;
        Resource = resource;
        Alignment = alignment;
        Armor = armor;
        Resistance = resistance;
        Conduit = conduit;
        AggroRange = 5f;

        Minap = minap;
        Maxap = maxap;
        if (m_maxas == 0 || m_minas == 0)
            M_Attackspeed = 0f;
        else
            M_Attackspeed = m_maxas / m_minas;

        Name = name;
        Title = title;
        if (association != string.Empty)
            Association = "<" + association + ">";
        else
            Association = string.Empty;
    }

    public Mob()
    {
        this.ID = -1;
    }
}