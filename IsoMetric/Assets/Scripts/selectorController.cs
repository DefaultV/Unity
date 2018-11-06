using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectorController : MonoBehaviour {
    float translation;
    float rotation;
    worldBuilder wb;
    SoundBox soundbox;
    GameObject currentTile;
    UIController UI_C;
    ItemDatabase ItemDB;
    public GameObject currentCreatureSelected;
    public GameObject currentCreatureViewed;
    public GameObject ok;
    public GameObject ok_atk;
    int c_X = 0;
    int c_Y = 0;
    List<GameObject> tileReachList = new List<GameObject>();

    int c_spellRange;
    int c_spellAoe;

    bool creatureSelected = false;
    bool moveMode = false;

    bool playMode = false;
    bool playMode_Enabled = false;
    void Start () {
        wb = GameObject.Find("WorldBuilder").GetComponent<worldBuilder>();
        transform.position = GameObject.Find("0,0").transform.position;
        UI_C = GameObject.Find("Canvas").transform.GetChild(0).gameObject.GetComponent<UIController>();
        soundbox = GameObject.Find("SoundCube").GetComponent<SoundBox>();
        ItemDB = GameObject.Find("GameController").GetComponent<ItemDatabase>();
	}
	
	// Update is called once per frame
	void Update () {
        translation = Input.GetAxis("Vertical");
        rotation = Input.GetAxis("Horizontal");

        if (playMode)
        {
            EnterPlayMode();
        }
        else
        {
            EnterDebugMode();
        }

        if (Input.GetKeyDown(KeyCode.W) && !playMode)
        {
            SetSelectorPosition(c_X-1, c_Y);
            soundbox.play(soundbox.selectMove);
        }
        if (Input.GetKeyDown(KeyCode.A) && !playMode)
        {
            SetSelectorPosition(c_X, c_Y-1);
            soundbox.play(soundbox.selectMove);
        }
        if (Input.GetKeyDown(KeyCode.S) && !playMode)
        {
            SetSelectorPosition(c_X+1, c_Y);
            soundbox.play(soundbox.selectMove);
        }
        if (Input.GetKeyDown(KeyCode.D) && !playMode)
        {
            SetSelectorPosition(c_X, c_Y+1);
            soundbox.play(soundbox.selectMove);
        }

        //DEBUG TESTING
        if (Input.GetKeyDown(KeyCode.Space) && !playMode)
        {
            if (creatureSelected) { 
                MoveCreatureToTile(c_X, c_Y);}
            else
                SelectCreatureOnTile(c_X, c_Y, 1);
        }
        //DEBUG TESTING
        if (Input.GetKeyDown(KeyCode.K) && !playMode)
        {
            if (creatureSelected)
                AttackCreatureMelee(c_X, c_Y);
        }
        if (Input.GetKeyDown(KeyCode.L) && !playMode)
        {
            if (creatureSelected)
                AttackCreatureSpell(c_X, c_Y, currentCreatureSelected.GetComponent<CreatureClass>().getSpells()[0]);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            playMode = !playMode;
        }
        Controls();
    }

    bool isAxisInUse_FIRE1 = false;
    bool isAxisInUse_FIRE2 = false;
    bool isAxisInUse_FIRE3 = false;
    bool isAxisInUse_JUMP = false;
    bool isAxisInUse_UP = false;
    bool isAxisInUse_DOWN = false;
    bool isAxisInUse_LEFT = false;
    bool isAxisInUse_RIGHT = false;

    void oneClickButton(string Axis)
    {
        switch (Axis)
        {
            case "Fire1":
                if (Input.GetAxisRaw(Axis) > 0 && !isAxisInUse_FIRE1)
                {
                    if (!UI_C.GetPlayMode())
                        return;
                    //UI_C.MoveIntoCategory();
                    isAxisInUse_FIRE1 = true;
                }
                break;
            case "Fire2":
                if (Input.GetAxisRaw(Axis) > 0 && !isAxisInUse_FIRE2)
                {
                    if (!UI_C.GetPlayMode())
                        return;
                    if (moveMode)
                    {
                        regretMoveCreature();
                    }
                    else
                    {

                    }
                    //UI_C.MoveOutCategory();
                    isAxisInUse_FIRE2 = true;
                }
                break;
            case "Fire3":
                if (Input.GetAxisRaw(Axis) > 0 && !isAxisInUse_FIRE3)
                {
                    if (!UI_C.GetPlayMode())
                        return;
                    isAxisInUse_FIRE3 = true;
                }
                break;
            case "Jump":
                if (Input.GetAxisRaw(Axis) > 0 && !isAxisInUse_JUMP)
                {
                    if (!UI_C.GetPlayMode())
                        return;
                    isAxisInUse_JUMP = true;
                }
                break;

            case "UpDown":
                if (Input.GetAxisRaw(Axis) > 0 && !isAxisInUse_UP)
                {
                    if (!UI_C.GetPlayMode())
                        return;
                    if (moveMode)
                    {
                        SetSelectorPosition(c_X - 1, c_Y);
                        soundbox.play(soundbox.selectMove);
                    }
                    else
                    {
                        UI_C.MoveMenuSelectorUp();
                    }
                    isAxisInUse_UP = true;
                }
                if (Input.GetAxisRaw(Axis) < 0 && !isAxisInUse_DOWN)
                {
                    if (!UI_C.GetPlayMode())
                        return;
                    if (moveMode)
                    {
                        SetSelectorPosition(c_X + 1, c_Y);
                        soundbox.play(soundbox.selectMove);
                    }
                    else
                    {
                        UI_C.MoveMenuSelectorDown();
                    }
                    isAxisInUse_DOWN = true;
                }
                break;
            case "LeftRight":
                if (Input.GetAxisRaw(Axis) > 0 && !isAxisInUse_LEFT)
                {
                    if (!UI_C.GetPlayMode())
                        return;
                    if (moveMode)
                    {
                        SetSelectorPosition(c_X, c_Y + 1);
                        soundbox.play(soundbox.selectMove);
                    }
                    else
                    {
                        Debug.Log("working");
                    }
                    isAxisInUse_LEFT = true;
                }
                if (Input.GetAxisRaw(Axis) < 0 && !isAxisInUse_RIGHT)
                {
                    if (!UI_C.GetPlayMode())
                        return;
                    if (moveMode)
                    {
                        SetSelectorPosition(c_X, c_Y - 1);
                        soundbox.play(soundbox.selectMove);
                    }
                    else
                    {

                    }
                    isAxisInUse_RIGHT = true;
                }
                break;
        }
    }

    void Controls()
    {
        if (Input.GetAxisRaw("Fire1") > 0) // X
            oneClickButton("Fire1");
        if (Input.GetAxisRaw("Fire1") == 0)
            isAxisInUse_FIRE1 = false;

        if (Input.GetAxis("Fire2") > 0) // Circle
            oneClickButton("Fire2");
        if (Input.GetAxisRaw("Fire2") == 0)
            isAxisInUse_FIRE2 = false;

        if (Input.GetAxis("Fire3") > 0) // square
            oneClickButton("Fire3");
        if (Input.GetAxisRaw("Fire3") == 0)
            isAxisInUse_FIRE3 = false;

        if (Input.GetAxis("Jump") > 0) // Triangle
            oneClickButton("Jump");
        if (Input.GetAxisRaw("Jump") == 0)
            isAxisInUse_JUMP = false;

        if (Input.GetAxis("LeftRight") > 0) // Right
            oneClickButton("LeftRight");
        if (Input.GetAxis("LeftRight") == 0)
            isAxisInUse_LEFT = false;

        if (Input.GetAxis("LeftRight") < 0) // Left
            oneClickButton("LeftRight");
        if (Input.GetAxis("LeftRight") == 0)
            isAxisInUse_RIGHT = false;

        if (Input.GetAxisRaw("UpDown") > 0) // Up
            oneClickButton("UpDown");
        if (Input.GetAxisRaw("UpDown") == 0)
            isAxisInUse_UP = false;

        if (Input.GetAxisRaw("UpDown") < 0) // Down
            oneClickButton("UpDown");
        if (Input.GetAxisRaw("UpDown") == 0)
            isAxisInUse_DOWN = false;
    }

    public GameController GC;
    void EnterPlayMode()
    {
        if (playMode_Enabled)
            return;
        Debug.Log("Entering PLAYMODE");
        playMode_Enabled = true;
        GameObject[] sortedSpeedList = wb.GetListOfCurrentCreatures();
        string str_list = "";
        foreach(GameObject c in sortedSpeedList)
        {
            str_list += c.GetComponent<CreatureClass>().getName() + ", ";
        }
        Debug.Log(str_list);
        CreatureController cc = sortedSpeedList[0].GetComponent<CreatureController>();
        SetSelectorPosition(cc.getPosX(), cc.getPosY());
        //Show UI
        UI_C.ShowUI_PLAYMODE();
        GC.SetCreatureList(sortedSpeedList);
    }

    void EnterDebugMode()
    {
        if (!playMode && !playMode_Enabled)
            return;
        Debug.Log("Entering DEBUGMODE");
        playMode = false;
        playMode_Enabled = false;
        UI_C.HideUI_PLAYMODE();
    }

    void EnterAreaMode(int mode)
    {
        int x;
        int y;
        switch (mode)
        {
            case 1: // MOVE
                int moveLength = currentCreatureSelected.GetComponent<CreatureClass>().getMoveLength();
                x = currentCreatureSelected.GetComponent<CreatureController>().getPosX();
                y = currentCreatureSelected.GetComponent<CreatureController>().getPosY();
                GameObject[] tiles;
                tiles = GameObject.FindGameObjectsWithTag("Tile");
                foreach(GameObject tile in tiles)
                {
                    tileClass tc = tile.GetComponent<tileClass>();
                    //double calc = Math.Floor(Math.Sqrt(Math.Pow(x-tc.getX(),2) + Math.Pow(y-tc.getY(),2))); //Euclidean distance
                    int calc2 = Math.Abs(x - tc.getX()) + Math.Abs(y - tc.getY()); // Manhattan distance
                    if (calc2 <= moveLength)
                    {
                        if (Math.Abs(tc.GetHeight() - currentCreatureSelected.GetComponent<CreatureController>().getCurrentTile().GetComponent<tileClass>().GetHeight()) > 2 || tc.getCurrentCreature() != null)
                            continue;
                        tileReachList.Add(tc.gameObject);
                        //DEBUG
                        GameObject tmp = Instantiate(ok);
                        tmp.transform.SetParent(GameObject.Find("Selector_Cache").transform);
                        tmp.transform.position = tc.transform.position;
                        tmp.GetComponent<SpriteRenderer>().sortingOrder = 400;
                    }
                }
                break;
            case 2: // ATTACK MELEE
                int AtkLength = 1;
                x = currentCreatureSelected.GetComponent<CreatureController>().getPosX();
                y = currentCreatureSelected.GetComponent<CreatureController>().getPosY();
                GameObject[] tiles_atk;
                tiles_atk = GameObject.FindGameObjectsWithTag("Tile");
                foreach (GameObject tile in tiles_atk)
                {
                    tileClass tc = tile.GetComponent<tileClass>();
                    //double calc = Math.Floor(Math.Sqrt(Math.Pow(x-tc.getX(),2) + Math.Pow(y-tc.getY(),2))); //Euclidean distance
                    int calc2 = Math.Abs(x - tc.getX()) + Math.Abs(y - tc.getY()); // Manhattan distance
                    if (calc2 <= AtkLength)
                    {
                        tileReachList.Add(tc.gameObject);
                        //DEBUG
                        GameObject tmp = Instantiate(ok_atk);
                        tmp.transform.SetParent(GameObject.Find("Selector_Cache").transform);
                        tmp.transform.position = tc.transform.position;
                        tmp.GetComponent<SpriteRenderer>().sortingOrder = 400;
                    }
                }
                break;
            case 3: // ATTACK RANGED
                int BowLength = currentCreatureSelected.GetComponent<CreatureClass>().getMainHandBow().getRange();
                x = currentCreatureSelected.GetComponent<CreatureController>().getPosX();
                y = currentCreatureSelected.GetComponent<CreatureController>().getPosY();
                GameObject[] tiles_rng = GameObject.FindGameObjectsWithTag("Tile");
                foreach (GameObject tile in tiles_rng)
                {
                    tileClass tc = tile.GetComponent<tileClass>();
                    //double calc = Math.Floor(Math.Sqrt(Math.Pow(x-tc.getX(),2) + Math.Pow(y-tc.getY(),2))); //Euclidean distance
                    int calc2 = Math.Abs(x - tc.getX()) + Math.Abs(y - tc.getY()); // Manhattan distance
                    if (calc2 <= BowLength)
                    {
                        tileReachList.Add(tc.gameObject);
                        //DEBUG
                        GameObject tmp = Instantiate(ok_atk);
                        tmp.transform.SetParent(GameObject.Find("Selector_Cache").transform);
                        tmp.transform.position = tc.transform.position;
                        tmp.GetComponent<SpriteRenderer>().sortingOrder = 400;
                    }
                }
                break;
            case 4: // SPELL
                int spellLength = getCurrentSpellRange();
                int spellAOE = getCurrentSpellAoe();
                x = currentCreatureSelected.GetComponent<CreatureController>().getPosX();
                y = currentCreatureSelected.GetComponent<CreatureController>().getPosY();
                GameObject[] tiles_spell = GameObject.FindGameObjectsWithTag("Tile");
                foreach(GameObject tile in tiles_spell)
                {
                    tileClass tc = tile.GetComponent<tileClass>();
                    //double calc = Math.Floor(Math.Sqrt(Math.Pow(x-tc.getX(),2) + Math.Pow(y-tc.getY(),2))); //Euclidean distance
                    int calc2 = Math.Abs(x - tc.getX()) + Math.Abs(y - tc.getY()); // Manhattan distance
                    if (calc2 <= spellLength)
                    {
                        tileReachList.Add(tc.gameObject);
                        //DEBUG
                        GameObject tmp = Instantiate(ok);
                        tmp.transform.SetParent(GameObject.Find("Selector_Cache").transform);
                        tmp.transform.position = tc.transform.position;
                        tmp.GetComponent<SpriteRenderer>().sortingOrder = 400;
                    }

                    if (calc2 <= spellAOE)
                    {
                        GameObject tmp = Instantiate(ok_atk);
                        tmp.transform.SetParent(GameObject.Find("Selector_Cache_Main").transform);
                        tmp.transform.position = tc.transform.position;
                        tmp.GetComponent<SpriteRenderer>().sortingOrder = 400;
                    }
                }
                break;
        }
    }

    public void clearCache()
    {
        GameObject cache = GameObject.Find("Selector_Cache");
        GameObject s_cache = GameObject.Find("Selector_Cache_Main");
        for (int i = 0; i < cache.transform.childCount; i++)
        {
            Destroy(cache.transform.GetChild(i).gameObject);
        }
        tileReachList.Clear();
        for (int i = 0; i < s_cache.transform.childCount; i++)
        {
            Destroy(s_cache.transform.GetChild(i).gameObject);
        }
    }

    public bool AttackCreatureSpell(int x, int y, Spell spell)
    {
        GameObject defendingcreature = GameObject.Find(x.ToString() + "," + y.ToString()).GetComponent<tileClass>().getCurrentCreature();
        if (defendingcreature == null)
        {
            Debug.Log("No creature to attack");
            return false;
        }
        spell.trigger(defendingcreature.GetComponent<CreatureClass>());
        creatureSelected = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        //soundbox.play(soundbox.selectImpact);
        clearCache();
        Debug.Log("resetting to current creature: " + currentCreatureSelected.name);
        SetSelectorPosition(currentCreatureSelected.GetComponent<CreatureController>().getPosX(), currentCreatureSelected.GetComponent<CreatureController>().getPosY());
        currentCreatureSelected = null;
        GC.actionPointSpent();
        //soundbox.play(soundbox.Hit);
        return true;
    }

    public bool AttackCreatureRanged(int x, int y)
    {
        GameObject defendingcreature = GameObject.Find(x.ToString() + "," + y.ToString()).GetComponent<tileClass>().getCurrentCreature();
        if (defendingcreature == null)
        {
            Debug.Log("No creature to attack");
            return false;
        }
        //trigger attack
        currentCreatureSelected.GetComponent<CreatureClass>().AttackCreatureRanged(defendingcreature.GetComponent<CreatureClass>());
        creatureSelected = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        //soundbox.play(soundbox.selectImpact);
        clearCache();
        Debug.Log("resetting to current creature: " + currentCreatureSelected.name);
        SetSelectorPosition(currentCreatureSelected.GetComponent<CreatureController>().getPosX(), currentCreatureSelected.GetComponent<CreatureController>().getPosY());
        currentCreatureSelected = null;
        GC.actionPointSpent();
        //soundbox.play(soundbox.Hit);
        return true;
    }

    public void AttackCreatureSpell_stockhold()
    {
        creatureSelected = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        clearCache();
        SetSelectorPosition(currentCreatureSelected.GetComponent<CreatureController>().getPosX(), currentCreatureSelected.GetComponent<CreatureController>().getPosY());
        currentCreatureSelected = null;
    }

    public void UseItem(Item item)
    {

    }

    public bool UsePotion(Potion potion)
    {
        //trigger potion
        potion.triggerEffect(currentCreatureSelected.GetComponent<CreatureClass>());
        creatureSelected = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        //soundbox.play(soundbox.selectImpact);
        clearCache();
        Debug.Log("resetting to current creature: " + currentCreatureSelected.name);
        SetSelectorPosition(currentCreatureSelected.GetComponent<CreatureController>().getPosX(), currentCreatureSelected.GetComponent<CreatureController>().getPosY());
        currentCreatureSelected = null;
        GC.actionPointSpent();
        //soundbox.play(soundbox.Hit);
        return true;
    }

    public bool AttackCreatureMelee(int x, int y)
    {
        GameObject defendingcreature = GameObject.Find(x.ToString() + "," + y.ToString()).GetComponent<tileClass>().getCurrentCreature();
        if (defendingcreature == null)
        {
            Debug.Log("No creature to attack");
            return false;
        }
        currentCreatureSelected.GetComponent<CreatureClass>().AttackCreatureMelee(defendingcreature.GetComponent<CreatureClass>());
        creatureSelected = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        soundbox.play(soundbox.selectImpact);
        clearCache();
        Debug.Log("resetting to current creature: " + currentCreatureSelected.name);
        SetSelectorPosition(currentCreatureSelected.GetComponent<CreatureController>().getPosX(), currentCreatureSelected.GetComponent<CreatureController>().getPosY());
        currentCreatureSelected = null;
        GC.actionPointSpent();
        soundbox.play(soundbox.Hit);
        return true;
    }

    void ViewCreatureOnTile(int x, int y)
    {
        try
        {
            currentCreatureViewed = GameObject.Find(x.ToString() + "," + y.ToString()).GetComponent<tileClass>().getCurrentCreature();
            if (currentCreatureViewed == null)
            {
                UI_C.UpdateUI(currentCreatureViewed, false);
                return;
            }
            UI_C.UpdateUI(currentCreatureViewed, true);
            Debug.Log("Creature selected(view): " + currentCreatureViewed + ", on tile: " + x.ToString() + "," + y.ToString());
        }
        catch (Exception e)
        {
            Debug.Log("Couldn't retrieve creature, probably NULL; " + e.Message);
            currentCreatureViewed = null;
            UI_C.UpdateUI(currentCreatureViewed, false);
        }
    }
    public void SelectCreatureOnTile(int x, int y, int mode)
    {
        try
        {
            currentCreatureSelected = GameObject.Find(x.ToString() + "," + y.ToString()).GetComponent<tileClass>().getCurrentCreature();
            if (currentCreatureSelected == null)
                return;
            EnterAreaMode(mode);
            GetComponent<SpriteRenderer>().color = Color.cyan;
            creatureSelected = true;
            Debug.Log("Creature selected: " + currentCreatureSelected + ", on tile: "+x.ToString() +"," + y.ToString());
            soundbox.play(soundbox.selectImpact_R);
        }
        catch (Exception e)
        {
            Debug.Log("Couldn't retrieve creature, probably NULL; " + e.Message);
            creatureSelected = false;
            currentCreatureSelected = null;
        }
    }

    public bool MoveCreatureToTile(int x, int y)
    {
        if (!tileReachList.Contains(GameObject.Find(x.ToString() + "," + y.ToString())))
        {
            Debug.Log("Tile OOR");
            return false;
        }
        currentCreatureSelected.GetComponent<CreatureController>().SetCreaturePosition(x, y);
        creatureSelected = false;
        currentCreatureSelected = null;
        GetComponent<SpriteRenderer>().color = Color.white;
        soundbox.play(soundbox.selectImpact);
        clearCache();
        ViewCreatureOnTile(c_X, c_Y);
        GC.actionPointSpent();
        return true;
    }

    public void regretMoveCreature()
    {
        clearCache();
        CreatureController cc = currentCreatureSelected.GetComponent<CreatureController>();
        SetSelectorPosition(cc.getPosX(), cc.getPosY());
        currentCreatureSelected = null;
        UI_C.ShowUI_PLAYMODE();
        setMoveMode(false);
        GetComponent<SpriteRenderer>().color = Color.white;
        creatureSelected = false;
    }

    public void SetSelectorPosition(int x, int y)
    {
        float tileHeight = 0f;
        int oc_X = c_X;
        int oc_Y = c_Y;
        c_X = x;
        c_Y = y;
        if (x < 0)
            c_X = 0;
        if (y < 0)
            c_Y = 0;
        try
        {
            currentTile = GameObject.Find(x.ToString() + "," + y.ToString());
            tileHeight = ((currentTile.GetComponent<tileClass>().Height - 1) * 0.1f);
            if (tileHeight > 0)
                GetComponent<SpriteRenderer>().sortingOrder = currentTile.GetComponent<SpriteRenderer>().sortingOrder + 40;
            else
                GetComponent<SpriteRenderer>().sortingOrder = currentTile.GetComponent<SpriteRenderer>().sortingOrder + 10;
        } catch (Exception e)
        {
            Debug.Log(e.Message);
            c_X = oc_X;
            c_Y = oc_Y;
            tileHeight = 0f;
        }
        transform.position = new Vector2(currentTile.transform.position.x, currentTile.transform.position.y + tileHeight);
        Debug.Log("Selector moving to tile: " + x.ToString() + "," + y.ToString());
        ViewCreatureOnTile(c_X, c_Y);
    }

    public int getX()
    {
        return this.c_X;
    }

    public int getY()
    {
        return this.c_Y;
    }

    public bool isCreatureSelected()
    {
        return this.creatureSelected;
    }

    public void setMoveMode(bool n)
    {
        this.moveMode = n;
    }

    public int GetCreatureWeaponType(int view)
    {
        if (view == 0)
        {
            CreatureClass cc = currentCreatureSelected.GetComponent<CreatureClass>();
            return cc.getMainHandType();
        }
        else
        {
            CreatureClass cc = getCreatureHover();
            return cc.getMainHandType();
        }
    }

    public CreatureClass getCreatureHover()
    {
        tileClass tc = GameObject.Find(getX().ToString() + "," + getY().ToString()).GetComponent<tileClass>();
        return tc.getCurrentCreature().GetComponent<CreatureClass>();
    }

    public int getCurrentSpellRange()
    {
        return this.c_spellRange;
    }
    public int getCurrentSpellAoe()
    {
        return this.c_spellAoe;
    }
    public void setCurrentSpellRange(int range)
    {
        this.c_spellRange = range;
    }
    public void setCurrentSpellAoe(int aoe)
    {
        this.c_spellAoe = aoe;
    }
}
