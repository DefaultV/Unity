using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public GameObject UI_Char_sprite;
    public GameObject UI_Char_name;
    public GameObject UI_Char_hp;
    public GameObject UI_Char_arcana;
    public GameObject UI_Char_exp;
    public GameObject UI_PLAYMODE;
    public GameObject UI_Menu_Selector;
    public GameObject UI_ActionPoints;
    public selectorController SC;
    public GameController GC;
    public SoundBox SB;
    bool UI_Show = false;

    Vector3 init_panel_pos;

    void Start () {
        SC = SC.GetComponent<selectorController>();
        init_panel_pos = gameObject.GetComponent<RectTransform>().localPosition;
        ShowUI(false);

        /*for (int i = 0; i < GameObject.Find("UI_Playmode_panel").transform.GetChild(0).transform.childCount; i++)
        {
            GameObject tmp = GameObject.Find("UI_Playmode_panel").transform.GetChild(0).transform.GetChild(i).gameObject;
            for (int k = 0; k < tmp.transform.childCount; k++)
            {
                tmp.transform.GetChild(k).gameObject.SetActive(false);
            }
        }*/

        UpdateUI_PLAYMODE();
        HideUI_PLAYMODE();
    }
	
    
	void Update () {
		if (gameObject.GetComponent<RectTransform>().localPosition.x <= init_panel_pos.x)
        {
            gameObject.GetComponent<RectTransform>().localPosition = new Vector3(gameObject.GetComponent<RectTransform>().localPosition.x + 20, gameObject.GetComponent<RectTransform>().localPosition.y, gameObject.GetComponent<RectTransform>().localPosition.z);
        }
	}

    void SetName(string name)
    {
        UI_Char_name.GetComponent<Text>().text = name;
    }
    void SetHealth(int health, int h_max)
    {
        Vector3 vec = new Vector3((float)health / (float)h_max, 1, 1);
        UI_Char_hp.transform.GetChild(1).GetComponent<RectTransform>().localScale = vec;
        UI_Char_hp.transform.GetChild(2).GetComponent<Text>().text = health.ToString() + "/" + h_max.ToString();
    }
    void SetArcana(int arcana, int a_max)
    {
        Vector3 vec = new Vector3((float)arcana / (float)a_max, 1, 1);
        UI_Char_arcana.transform.GetChild(1).GetComponent<RectTransform>().localScale = vec;
        UI_Char_arcana.transform.GetChild(2).GetComponent<Text>().text = arcana.ToString() + "/" + a_max.ToString();
    }
    void SetExp(int exp, int e_max)
    {
        Vector3 vec = new Vector3((float)exp / (float)e_max, 1, 1);
        UI_Char_exp.transform.GetChild(1).GetComponent<RectTransform>().localScale = vec;
        UI_Char_exp.transform.GetChild(2).GetComponent<Text>().text = exp.ToString() + "/" + e_max.ToString();
    }
    void SetSprite(Sprite c_sprite)
    {
        UI_Char_sprite.GetComponent<Image>().sprite = c_sprite;
    }

    public void UpdateUI(GameObject Creature, bool show)
    {
        if (Creature == null)
        {
            ShowUI(false);
            return;
        }
            
        if (show == false)
            ShowUI(false);
        else
            ShowUI(true);
        CreatureClass cCreature = Creature.GetComponent<CreatureClass>();
        SetName(cCreature.getName());
        SetHealth(cCreature.getHealth(), cCreature.getMaxHealth());
        SetArcana(cCreature.getArcana(), cCreature.getMaxArcana());
        SetExp(cCreature.getExp(), cCreature.getMaxExp());
        SetSprite(cCreature.getSprite());
    }

    public void ShowUI(bool n)
    {
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(-550, gameObject.GetComponent<RectTransform>().localPosition.y, gameObject.GetComponent<RectTransform>().localPosition.z);
        gameObject.SetActive(n);
    }

    Spell selectedSpell;
    GameObject currentCategory;
    int currentCategoryChild = 0;
    GameObject currentItem;
    int childLengthItem;
    int childLengthCategory;
    int currentItemChild = 0;
    bool chosenCategory = false;
    bool chosenSpell = false;
    bool resolveSpell = false;
    public void UpdateUI_PLAYMODE()
    {
        /*currentCategory = GameObject.Find("UI_Playmode_panel").transform.GetChild(0).transform.GetChild(currentCategoryChild).gameObject;
        if (chosenCategory)
        {
            ShowCategory(currentCategory, true);
            currentItem = currentCategory.transform.GetChild(currentItemChild).gameObject;
        }
        childLengthItem = currentCategory.transform.childCount;
        childLengthCategory = GameObject.Find("UI_Playmode_panel").transform.GetChild(0).transform.childCount;
        //Debug.Log("currently selecting: " + currentCategory);
        //Debug.Log("currently selecting item: " + currentCategory.transform.GetChild(currentItemChild).gameObject);
        MoveMenuSelector(currentCategory);*/

    }

    bool playmode = false;
    public void ShowUI_PLAYMODE()
    {
        UI_PLAYMODE.SetActive(true);
        playmode = true;
    }

    public void HideUI_PLAYMODE()
    {
        UI_PLAYMODE.SetActive(false);
        //playmode = false;
    }

    public bool GetPlayMode()
    {
        return this.playmode;
    }

    public void ShowCategory(GameObject category, bool n)
    {
        for (int i = 0; i < category.transform.childCount-1; i++)
        {
            category.transform.GetChild(i).gameObject.SetActive(n);
        }
    }

    //HACKFIX
    public GameObject SpellPanel;
    public GameObject SpellList;
    public void ShowSpells(bool n)
    {
        SpellPanel.SetActive(n);
        SpellList.SetActive(n);
    }

    public void MoveMenuSelector(GameObject category)
    {
        currentCategory = category;
        //CI = currentItem;
        UI_Menu_Selector.transform.SetParent(currentCategory.transform);
        UI_Menu_Selector.transform.localPosition = Vector3.zero;
        if (chosenCategory)
            UI_Menu_Selector.transform.localPosition = new Vector3(currentItem.transform.localPosition.x - 40, currentItem.transform.localPosition.y, 0);
        else if (!chosenCategory)
            UI_Menu_Selector.transform.localPosition = new Vector3(-100, 0, 0);
        if (chosenSpell)
        {
            currentCategory = GameObject.Find("SpellPanel");
            UI_Menu_Selector.transform.SetParent(currentCategory.transform);
            //Debug.Log(currentCategory.transform.GetChild(0).transform.GetChild(currentItemChild).name);
            string spellName = currentCategory.transform.GetChild(0).transform.GetChild(currentItemChild).name;
            Debug.Log("Moving to: '" + spellName+"'");
            Transform overrideChild = currentCategory.transform.GetChild(0).transform.GetChild(currentItemChild);
            UI_Menu_Selector.transform.localPosition = new Vector3(overrideChild.localPosition.x - 40, overrideChild.localPosition.y, 0);
            SetSelectedSpell(retrieveCreatureSpellFromList(spellName, SC.getCreatureHover()));
        }
    }

    public void FreezeCameraAndAction()
    {

    }

    public void TriggerCategory_Move()
    {
        if (GC.getActionPoints() <= 0)
            return;
        if (!SC.isCreatureSelected())
        {
            SC.SelectCreatureOnTile(SC.getX(), SC.getY(), 1);
            SC.setMoveMode(true);
            HideUI_PLAYMODE();
        }
        else
        {
            if (!SC.MoveCreatureToTile(SC.getX(), SC.getY()))
            {
                return;
            }
            SC.setMoveMode(false);
            ShowUI_PLAYMODE();
        }
    }

    public void TriggerCategory_Attack()
    {
        if (GC.getActionPoints() <= 0)
            return;
        if (!SC.isCreatureSelected())
        {
            if (SC.GetCreatureWeaponType(1) == 1 || SC.GetCreatureWeaponType(1) == 0)
                SC.SelectCreatureOnTile(SC.getX(), SC.getY(), 2);
            else if (SC.GetCreatureWeaponType(1) == 2)
            {
                Debug.Log("Bow equipped, entering bow mode");
                SC.SelectCreatureOnTile(SC.getX(), SC.getY(), 3);
            }
            SC.setMoveMode(true);
            HideUI_PLAYMODE();
        }
        else
        {
            //Debug.Log(SC.GetCreatureWeaponType(0));
            //Debug.Log(SC.GetCreatureWeaponType(1));
            if (SC.GetCreatureWeaponType(0) == 1 || SC.GetCreatureWeaponType(0) == 0)
            {
                if (!SC.AttackCreatureMelee(SC.getX(), SC.getY()))
                    return;
            }
            else if (SC.GetCreatureWeaponType(0) == 2)
            {
                if (!SC.AttackCreatureRanged(SC.getX(), SC.getY()))
                    return;
            }

            SC.setMoveMode(false);
            ShowUI_PLAYMODE();
        }
    }

    public void TriggerCategory_Spell()
    {
        if (GC.getActionPoints() <= 0)
            return;
        //Do UI stuff
        ShowSpells(true);
        if (!chosenSpell)
        {
            chosenSpell = true;
            currentItemChild = 0;
            MoveMenuSelector(GameObject.Find("SpellPanel"));
            Debug.Log("Moving ...");
        }
        else if (chosenSpell)
        {
            if (resolveSpell)
            {
                GameObject[] spell_tiles = GameObject.FindGameObjectsWithTag("Tile");
                foreach (GameObject tile in spell_tiles)
                {
                    tileClass tc = tile.GetComponent<tileClass>();
                    int calc2 = Math.Abs(SC.getX() - tc.getX()) + Math.Abs(SC.getY() - tc.getY()); // Manhattan distance
                    if (calc2 <= SC.getCurrentSpellAoe())
                    {
                        if (tc.getCurrentCreature() != null)
                        {
                            GetSelectedSpell().trigger(tc.getCurrentCreature().GetComponent<CreatureClass>());
                            Debug.Log("Triggering spell on creature in range: " + tc.getCurrentCreature().GetComponent<CreatureClass>().getName());
                        }
                    }
                }
                Debug.Log("Resolving spell");
                chosenSpell = false;
                resolveSpell = false;
                SC.setMoveMode(false);
                SC.clearCache();
                //HACKFIX
                //REDACTED resetPointer();
                //currentCategory = GameObject.Find("UI_Playmode_panel").transform.GetChild(0).transform.GetChild(0).gameObject;
                ShowUI_PLAYMODE();
                UpdateUI_PLAYMODE();
                //REDACTED MoveOutCategory();
                GC.actionPointSpent();
                SB.play(SB.SpellBuff);
                SC.AttackCreatureSpell_stockhold();
                //hideSkillBook();
            }
            else if (!resolveSpell)
            {
                SC.setCurrentSpellRange(GetSelectedSpell().getRange());
                SC.setCurrentSpellAoe(GetSelectedSpell().getAoE());
                SC.SelectCreatureOnTile(SC.getX(), SC.getY(), 4);
                SC.setMoveMode(true);
                HideUI_PLAYMODE();
                Debug.Log("setting spell: " + GetSelectedSpell().getSpellName());
                resolveSpell = true;
            }
            //END
        }
    }

    public void TriggerCategory_Item()
    {
        if (GC.getActionPoints() <= 0)
            return;
    }

    public void TriggerCategory_End()
    {
        GC.NextCreatureInQueue();
        //REDACTED MoveOutCategory();
    }

    /*public void MoveIntoCategory_REDACTED()
    {
        //RE-IMPLEMENT PROPERLY
        if (chosenCategory)
        {
            if (chosenSpell)
                currentItem.name = "Spell";
            Debug.Log("Choosing action: " + currentItem.name);
            switch (currentItem.name)
            {
                case "Move":
                    if (GC.getActionPoints() <= 0)
                        return;
                    if (!SC.isCreatureSelected())
                    {
                        SC.SelectCreatureOnTile(SC.getX(), SC.getY(),1);
                        SC.setMoveMode(true);
                        HideUI_PLAYMODE();
                    }
                    else
                    {
                        if (!SC.MoveCreatureToTile(SC.getX(), SC.getY()))
                        {
                            return;
                        }
                        SC.setMoveMode(false);
                        ShowUI_PLAYMODE();
                    }
                    break;
                case "Attack":
                    if (GC.getActionPoints() <= 0)
                        return;
                    if (!SC.isCreatureSelected())
                    {
                        if (SC.GetCreatureWeaponType(1) == 1 || SC.GetCreatureWeaponType(1) == 0)
                            SC.SelectCreatureOnTile(SC.getX(), SC.getY(), 2);
                        else if (SC.GetCreatureWeaponType(1) == 2)
                        {
                            Debug.Log("Bow equipped, entering bow mode");
                            SC.SelectCreatureOnTile(SC.getX(), SC.getY(), 3);
                        }
                        SC.setMoveMode(true);
                        HideUI_PLAYMODE();
                    }
                    else
                    {
                        //Debug.Log(SC.GetCreatureWeaponType(0));
                        //Debug.Log(SC.GetCreatureWeaponType(1));
                        if (SC.GetCreatureWeaponType(0) == 1 || SC.GetCreatureWeaponType(0) == 0)
                        {
                            if (!SC.AttackCreatureMelee(SC.getX(), SC.getY()))
                                return;
                        }
                        else if (SC.GetCreatureWeaponType(0) == 2)
                        {
                            if (!SC.AttackCreatureRanged(SC.getX(), SC.getY()))
                                return;
                        }

                        SC.setMoveMode(false);
                        ShowUI_PLAYMODE();
                    }
                    break;
                case "Spell":
                    if (GC.getActionPoints() <= 0)
                        return;
                    //Do UI stuff
                    ShowSpells(true);
                    if (!chosenSpell)
                    {
                        chosenSpell = true;
                        currentItemChild = 0;
                        MoveMenuSelector(GameObject.Find("SpellPanel"));
                        Debug.Log("Moving ...");
                    }
                    else if (chosenSpell)
                    {
                        if (resolveSpell)
                        {
                            GameObject[] spell_tiles = GameObject.FindGameObjectsWithTag("Tile");
                            foreach (GameObject tile in spell_tiles)
                            {
                                tileClass tc = tile.GetComponent<tileClass>();
                                int calc2 = Math.Abs(SC.getX() - tc.getX()) + Math.Abs(SC.getY() - tc.getY()); // Manhattan distance
                                if (calc2 <= SC.getCurrentSpellAoe())
                                {
                                    if (tc.getCurrentCreature() != null)
                                    {
                                        GetSelectedSpell().trigger(tc.getCurrentCreature().GetComponent<CreatureClass>());
                                        Debug.Log("Triggering spell on creature in range: " + tc.getCurrentCreature().GetComponent<CreatureClass>().getName());
                                    }
                                }
                            }
                            Debug.Log("Resolving spell");
                            chosenSpell = false;
                            resolveSpell = false;
                            SC.setMoveMode(false);
                            SC.clearCache();
                            //HACKFIX
                            //REDACTED resetPointer();
                            //currentCategory = GameObject.Find("UI_Playmode_panel").transform.GetChild(0).transform.GetChild(0).gameObject;
                            ShowUI_PLAYMODE();
                            UpdateUI_PLAYMODE();
                            //REDACTED MoveOutCategory();
                            GC.actionPointSpent();
                            SB.play(SB.SpellBuff);
                            SC.AttackCreatureSpell_stockhold();
                            //hideSkillBook();
                        }
                        else if (!resolveSpell)
                        {
                            SC.setCurrentSpellRange(GetSelectedSpell().getRange());
                            SC.setCurrentSpellAoe(GetSelectedSpell().getAoE());
                            SC.SelectCreatureOnTile(SC.getX(), SC.getY(), 4);
                            SC.setMoveMode(true);
                            HideUI_PLAYMODE();
                            Debug.Log("setting spell: " + GetSelectedSpell().getSpellName());
                            resolveSpell = true;
                        }
                        //END
                    }
                    //else if (resolving): foreach creature in AOE do SC.AttackCreatureSpell(aoe.x,aoe.y);
                    //Menu wrapper
                    //SpellWrapperMenu();
                    break;
                case "Item":
                    if (GC.getActionPoints() <= 0)
                        return;
                    break;
                case "End":
                    GC.NextCreatureInQueue();
                    //MoveOutCategory(); REDACTED
                    break;
            }
            return;
        }
        chosenCategory = true;
        UpdateUI_PLAYMODE();
        SB.play(SB.selectMove);
    }*/

    /*void resetPointer() REDACTED
    {
        chosenCategory = false;
        chosenSpell = false;
        currentCategory = null;
        currentCategoryChild = 0;
        currentItem = null;
        currentItemChild = 0;
        resolveSpell = false;
        ShowSpells(false);
    }*/

    /*public void MoveOutCategory() REDACTED
    {
        currentItemChild = 0;
        currentCategoryChild = 0;
        ShowCategory(currentCategory, false);
        chosenCategory = false;
        resetPointer();
        UpdateUI_PLAYMODE();
        SB.play(SB.selectMove);
    }*/

    public void MoveMenuSelectorUp()
    {
        if (!chosenCategory && currentCategoryChild <= 0)
            return;
        if (chosenCategory && currentItemChild <= 0)
            return;
        if (!chosenCategory)
            currentCategoryChild--;
        else
            currentItemChild--;
        UpdateUI_PLAYMODE();
        SB.play(SB.selectMove);
    }

    public void MoveMenuSelectorDown()
    {
        if (!chosenCategory && currentCategoryChild == childLengthCategory - 1)
            return;
        if (chosenCategory && currentItemChild == childLengthItem - 2)
            return;
        if (!chosenCategory)
            currentCategoryChild++;
        else
            currentItemChild++;
        UpdateUI_PLAYMODE();
        SB.play(SB.selectMove);
    }

    public void UpdateActionPoints(int actionpoints)
    {
        UI_ActionPoints.GetComponent<Text>().text = GC.getActionPoints().ToString();
    }

    public void SetSelectedSpell(Spell spell)
    {
        this.selectedSpell = spell;
    }

    //For spell selection!
    void SpellWrapperMenu()
    {
        if (!SC.isCreatureSelected())
        {
            SC.SelectCreatureOnTile(SC.getX(), SC.getY(), 3);
            SC.setMoveMode(true);
            HideUI_PLAYMODE();
        }
        else
        {
            //SELECTED SPELL IS NULL, FIX
            if (!SC.AttackCreatureSpell(SC.getX(), SC.getY(), GetSelectedSpell()))
            {
                return;
            }
            SC.setMoveMode(false);
            ShowUI_PLAYMODE();

        }
    }

    Spell retrieveCreatureSpellFromList(string name, CreatureClass cc)
    {
        foreach(Spell spell in cc.getSpells())
        {
            //Debug.Log("list contains: " + spell.getSpellName());
            if (spell.getSpellName() == name)
            {
                return spell;
            }
        }
        Debug.Log("No such spell in creature");
        return null;
    }

    public Spell GetSelectedSpell()
    {
        return this.selectedSpell;
    }
}
