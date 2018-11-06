using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class worldBuilder : MonoBehaviour {
    public GameObject tile;
    GameObject wb;
    GameObject wbcache;
    void Start () {
        wb = GameObject.Find("WorldBuilder");
        wbcache = GameObject.Find("WorldBuilderCacheTiles");
        loadLevel("level1");
        GetHeightOfTile(0, 0);
        GetHeightOfTile(0, 1);
	}
	
	void Update () {
		
	}

    public GameObject[] GetListOfCurrentCreatures()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("Creature");
        //list.ToList<GameObject>();
        GameObject tmp;
        for (int k = 0; k < list.Length; k++)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (list[k].GetComponent<CreatureClass>().getSpeed() > list[i].GetComponent<CreatureClass>().getSpeed())
                {
                    tmp = list[i];
                    list[i] = list[k];
                    list[k] = tmp;
                }
            }
        }
        return list;
    }

    public int GetHeightOfTile(int x, int y)
    {
        tile = GameObject.Find(x.ToString() + "," + y.ToString());
        return tile.GetComponent<tileClass>().GetHeight();
    }
    void loadLevel(string file)
    {
        string filename = file;
        file = "Assets/Resources/Level/" + file + ".txt";
        int[][] list = File.ReadAllLines(file)
                   .Select(l => l.Split(' ').Select(i => int.Parse(i)).ToArray())
                   .ToArray();

        string file2 = "Assets/Resources/Level/" + filename + "_t.txt";
        int[][] list_t = File.ReadAllLines(file2)
                   .Select(l => l.Split(' ').Select(i => int.Parse(i)).ToArray())
                   .ToArray();
        //Debug.Log(list);
        float x = 0f;
        float y = 0f;
        float n = 0f;
        int nX = 0;
        int nY = 0;
        int z = 0;
        for (int i = 0; i < list.Length; i++)
        {
            x = n*-0.3f;
            y = n*-0.16f;
            for (int j = 0; j < list[i].Length; j++)
            {
                GameObject newTile = Instantiate(tile, new Vector3(x, y), Quaternion.identity);
                newTile.name = nX.ToString() + "," + nY.ToString();
                newTile.GetComponent<SpriteRenderer>().sortingOrder = z;
                newTile.transform.SetParent(wb.transform);

                newTile.GetComponent<tileClass>().setX(nX);
                newTile.GetComponent<tileClass>().setY(nY);
                //Debug.Log(newTile);
                //Debug.Log(list[i][j]);
                newTile.GetComponent<tileClass>().SetHeight(list[i][j]);
                newTile.GetComponent<tileClass>().setType(list_t[i][j]);
                float hY = 0f;
                for (int k = 0; k < list[i][j]-1; k++)
                {
                    hY += 0.1f;
                    GameObject newTile2 = Instantiate(tile, new Vector3(x, y+hY), Quaternion.identity);
                    newTile2.GetComponent<SpriteRenderer>().sortingOrder = z + k+20;
                    newTile2.transform.SetParent(wbcache.transform);
                    newTile2.tag = "TilePROP";
                }
                x += 0.3f;
                y -= 0.167f;
                nY++;
                z++;
            }
            n++;
            nX++;
            nY = 0;
        }
    }
}
