using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class SaveState : MonoBehaviour {

    // Use this for initialization
    GameObject player;
    GameObject environment;
    List<SaveData> w_saveData = new List<SaveData>();
    int score;

    private List<SaveData> Database = new List<SaveData>();
    private JsonData saveData;
    private JsonData write_saveData;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

	void Start () {
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged += FindVitalsInScene;
        Load();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Save()
    {
        write_saveData = JsonMapper.ToJson(Database);

        JsonWriter writer = new JsonWriter();
        writer.WriteArrayStart();
        
        for (int i = 0; i < Database.Count; i++)
        {
            writer.WriteObjectStart();
            writer.WritePropertyName("actid");
            writer.Write(FetchDataFromAct(i+1).GetActID());
            writer.WritePropertyName("score");
            writer.Write(FetchDataFromAct(i+1).GetScore());
            writer.WritePropertyName("bonus");
            writer.Write(FetchDataFromAct(i+1).GetBonus());
            writer.WritePropertyName("issave");
            writer.Write(1);
            writer.WriteObjectEnd();
        }

        writer.WriteArrayEnd();
        Debug.Log("Saved: " + writer.ToString());
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "sav.json"), writer.ToString());
    }

    void SaveEmpty()
    {
        JsonWriter writer = new JsonWriter();
        writer.WriteArrayStart();

        writer.WriteObjectStart();
        writer.WritePropertyName("actid");
        writer.Write(1);
        writer.WritePropertyName("score");
        writer.Write(0);
        writer.WritePropertyName("bonus");
        writer.Write(0);
        writer.WritePropertyName("issave");
        writer.Write(1);
        writer.WriteObjectEnd();
        writer.WriteArrayEnd();

        Debug.Log("Saved Empty: " + writer.ToString());
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "sav.json"), writer.ToString());
    }

    void FindVitalsInScene(UnityEngine.SceneManagement.Scene a, UnityEngine.SceneManagement.Scene b)
    {
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (obj.tag == "Player")
                player = obj;
            if (obj.name == "Environment")
                environment = obj;
        }
        Debug.Log("found player: " + player);
        Debug.Log("found environment: " + environment);
    }

    public void Complete()
    {
        score = player.GetComponent<PlaneController>().mainPlayer.GetScore();
        Debug.Log("setscore: " + score);
        WriteDataToAct(DetermineCurrentAct());
        Save();
    }

    void Load()
    {
        if (!File.Exists(Path.Combine(Application.persistentDataPath, "sav.json")))
        {
            File.Create(Path.Combine(Application.persistentDataPath, "sav.json")).Close();
            SaveEmpty();
            Load();
            return;
        }
        saveData = JsonMapper.ToObject(File.ReadAllText(Path.Combine(Application.persistentDataPath, "sav.json")));
        for (int i = 0; i < saveData.Count; i++)
        {
            Database.Add(new SaveData((int)saveData[i]["actid"], (int)saveData[i]["score"], (int)saveData[i]["bonus"], (int)saveData[i]["issave"]));
        }
        w_saveData = Database;
        Debug.Log("Loaded: " + saveData);
    }

    public SaveData FetchDataFromAct(int actid)
    {
        for (int i = 0; i < Database.Count; i++)
        {
            if (Database[i].GetActID() == actid)
                return Database[i];
        }
        return null;
    }

    public void WriteDataToAct(int actid)
    {
        Debug.Log("Writing data to act: " + actid);
        FetchDataFromAct(actid).SetScore(score);
        FetchDataFromAct(actid).SetBonus(0);
    }

    int DetermineCurrentAct()
    {
        switch (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
        {
            case "scene111":
                return 1;
            case "scene222":
                return 2;
        }
        return -1;
    }
}

public class SaveData
{
    int ActID { get; set; }
    int Score { get; set; }
    int Bonus { get; set; }
    int IsSave { get; set; }

    public SaveData()
    {
        ActID = -1;
    }

    public SaveData(int id, int score, int bonus, int save)
    {
        ActID = id;
        Score = score;
        Bonus = bonus;
        IsSave = save;
    }

    public bool IsSaveData()
    {
        if (IsSave == 1)
            return true;
        else
            return false;
    }

    public int GetActID()
    {
        return ActID;
    }

    public int GetScore()
    {
        return Score;
    }

    public void SetScore(int n)
    {
        Score = n;
    }

    public int GetBonus()
    {
        return Bonus;
    }

    public void SetBonus(int n)
    {
        Bonus = n;
    }
}