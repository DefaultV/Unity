using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureClass : MonoBehaviour {
    public GameObject damageText;
    SpriteRenderer sprite;
    ItemDatabase itemDB;
    List<Item> itemList = new List<Item>();
    List<Spell> spellList = new List<Spell>();
    Sword s_mainhand;
    Bow b_mainhand;
    bool controllable;
    int faction;

    string c_name;
    int health;
    int health_MAX;
    int arcana;
    int arcana_MAX;
    int exp;
    int exp_REQUIRED;
    //infusions, either boots etc...

    float speed;
    float speed_init;
    int move;
    int move_MAX;
    float atkPower;
    float atkPower_init;
    float atkArcana;
    float atkArcana_init;
    float defArmor;
    float defArmor_init;
    float critChance;
    float critChance_init;

    void Start () {
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        itemDB = GameObject.Find("GameController").GetComponent<ItemDatabase>();
        //DEBUG
        this.spellList = itemDB.spellDatabase;
    }
	
	void Update () {
        
    }

    public void AttackCreatureMelee(CreatureClass creature)
    {
        if (creature.getName() == this.getName())
        {
            Debug.Log("Can't attack yourself!");
            return;
        }
        float rnd = Random.Range(0, 100);
        float chance = rnd + critChance;
        int modifier = 1;
        int calcDamage = Mathf.FloorToInt((2 * Random.Range(this.getAtkPower(), this.getAtkPower()+5) - creature.getDefense()) * 0.5f);
        if (chance >= 80)
            modifier = modifier * 2;
        creature.takeDamage(calcDamage*modifier);
        //Debug.Log("Attacked creature: " + creature + " with " + calcDamage*modifier + " damage " + "cc: " + chance);
        GameObject tmp_txt = Instantiate(damageText);
        tmp_txt.GetComponent<Text_Damage>().setPos(creature.transform.position);
        tmp_txt.GetComponent<Text_Damage>().setText("-"+(calcDamage*modifier).ToString());
    }

    public void AttackCreatureRanged(CreatureClass creature)
    {
        if (creature.getName() == this.getName())
        {
            Debug.Log("Can't attack yourself!");
            return;
        }
        Debug.Log("attacking ranged");
        //IMPLEMENT
        return;
    }

    public void SetupNewCreature(bool controllable, int faction, string c_name,
        int health, int arcana, int exp, float speed,
        int move, float atkPower, float atkArcana, float defArmor, float critChance)
    {
        this.controllable = controllable;
        this.faction = faction;
        this.c_name = c_name;
        this.health = health;
        this.health_MAX = health;

        this.arcana = arcana;
        this.arcana_MAX = arcana;
        this.exp = exp;
        this.exp_REQUIRED = exp*2;

        this.speed = speed;
        this.speed_init = speed;
        this.move = move;
        this.move_MAX = move;
        this.atkPower = atkPower;
        this.atkPower_init = atkPower;
        this.atkArcana = atkArcana;
        this.atkArcana_init = atkArcana;
        this.defArmor = defArmor;
        this.defArmor_init = defArmor;
        this.critChance = critChance;
        this.critChance_init = critChance;
    }

    public float getSpeed()
    {
        return this.speed;
    }

    public Sprite getSprite()
    {
        return this.sprite.sprite;
    }

    public int getMoveLength()
    {
        return this.move;
    }

    public string getName()
    {
        return this.c_name;
    }

    public void setName(string name)
    {
        this.c_name = name;
    }

    public bool isControllable()
    {
        return this.controllable;
    }

    public void setControllable(bool control)
    {
        this.controllable = control;
    }

    public int getFaction()
    {
        return this.faction;
    }

    public void takeHealing(int heal)
    {
        this.health += heal;
    }

    public void takeDamage(int damage)
    {
        this.health -= damage;
    }

    public void spendArcana(int amount)
    {
        this.arcana -= amount;
    }

    public void fillArcana(int amount)
    {
        this.arcana += amount;
    }

    public void setHealth(int health)
    {
        this.health = health;
        this.health_MAX = this.health;
    }
    public void setArcana(int arcana)
    {
        this.arcana = arcana;
        this.arcana_MAX = this.arcana;
    }
    public void setExp(int exp)
    {
        this.exp = exp;
    }

    public int getHealth()
    {
        return this.health;
    }
    public int getMaxHealth()
    {
        return this.health_MAX;
    }

    public int getArcana()
    {
        return this.arcana;
    }
    public int getMaxArcana()
    {
        return this.arcana_MAX;
    }

    public int getExp()
    {
        return this.exp;
    }
    public int getMaxExp()
    {
        return this.exp_REQUIRED;
    }

    public float getDefense()
    {
        return this.defArmor;
    }
    public void setDefense(float defense)
    {
        this.defArmor = defense;
    }

    public float getAtkPower()
    {
        return this.atkPower;
    }

    public List<Item> getItems()
    {
        return this.itemList;
    }
    public Item getSpecificItem(string name)
    {
        return null;
    }

    public List<Spell> getSpells()
    {
        return this.spellList;
    }
    public Spell getSpecificSpell(string name)
    {
        return null;
    }

    public void setMainHandSword(Sword item)
    {
        this.s_mainhand = item;
    }
    public void setMainHandBow(Bow item)
    {
        this.b_mainhand = item;
    }
    public Bow getMainHandBow()
    {
        return b_mainhand;
    }
    public Sword getMainHandSword()
    {
        return s_mainhand;
    }

    public int getMainHandType()
    {
        if (s_mainhand != null && b_mainhand == null)
        {
            return 1;
        }
        if (b_mainhand != null && s_mainhand == null)
        {
            return 2;
        }
        return 0;
    }
}
