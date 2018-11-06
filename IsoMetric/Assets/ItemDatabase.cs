using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
    public List<Item> itemDatabase = new List<Item>();
    public List<Spell> spellDatabase = new List<Spell>();
    void Start () {
        Spell FireballTEST = new Spell("Fireball_DEBUG",100,1,15,4,2);
        spellDatabase.Add(FireballTEST);
    }
	void Update () {}
}

public class Item
{
    Sprite sprite;
    int stacksize;
    int charges;

    public Item() { }

    public Item(Sprite sprite, int stacksize, int charges)
    {
        this.sprite = sprite;
        this.stacksize = stacksize;
        this.charges = charges;
    }

    void setStacksize(int stacksize)
    {
        this.stacksize = stacksize;
    }
    int getStacksize()
    {
        return this.stacksize;
    }
    void setSprite(Sprite sprite)
    {
        this.sprite = sprite;
    }
    Sprite getSprite()
    {
        return this.sprite;
    }
    void setCharges(int charges)
    {
        this.charges = charges;
    }
    int getCharges()
    {
        return this.charges;
    }
}

public class Sword : Item
{
    string name;
    int damagemod;
    int type;
    int ID;
    Effect effect;

    public Sword(string name, int damagemod, int type, int id)
    {
        this.name = name;
        this.damagemod = damagemod;
        this.type = type;
        this.ID = id;
    }
    public Sword(string name, int damagemod, int type, int id, int etype, int eval)
    {
        this.name = name;
        this.damagemod = damagemod;
        this.type = type;
        this.ID = id;
        this.setEffect(etype, eval);
    }

    string getName()
    {
        return this.name;
    }
    int getDamagemod()
    {
        return this.damagemod;
    }
    int getID()
    {
        return this.ID;
    }
    int getType()
    {
        return this.type;
    }
    public Effect getEffect()
    {
        return this.effect;
    }
    public void setEffect(int etype, int eval)
    {
        this.effect = new Effect(etype, eval);
    }
}

public class Bow : Item
{
    string name;
    int damagemod;
    int ID;
    int type;
    int range;
    Effect effect;

    public Bow(string name, int id, int type, int range, int damagemod)
    {
        this.damagemod = damagemod;
        this.name = name;
        this.ID = id;
        this.type = type;
        this.range = range;
    }
    public Bow(string name, int id, int type, int range, int damagemod, int etype, int eval)
    {
        this.damagemod = damagemod;
        this.name = name;
        this.ID = id;
        this.type = type;
        this.range = range;
        this.setEffect(etype, eval);
    }
    int getDamagemod()
    {
        return this.damagemod;
    }
    public int getRange()
    {
        return this.range;
    }
    public string getName() {
        return this.name;
    }
    public int getID() {
        return this.ID;
    }
    public int getType()
    {
        return this.type;
    }
    public Effect getEffect() {
        return this.effect;
    }
    public void setEffect(int etype, int eval)
    {
        this.effect = new Effect(etype, eval);
    }
}

public class Potion : Item
{
    string name;
    int id;
    Spell spell;
    public Potion(string name, int id, Spell spell)
    {
        this.name = name;
        this.id = id;
        this.spell = spell;
    }
    Spell getSpell()
    {
        return this.spell;
    }
    public void triggerEffect(CreatureClass cc)
    {
        getSpell().trigger(cc);
    }
}

public class Spell : Item
{
    string name;
    int ID;
    int range;
    int AoE;
    Effect effect;
    public int getRange()
    {
        return this.range;
    }
    public int getAoE()
    {
        return this.AoE;
    }
    public string getSpellName()
    {
        return this.name;
    }
    public int getID()
    {
        return this.ID;
    }
    public Effect getEffect()
    {
        return this.effect;
    }
    public Spell(string name, int id, int etype, int eval, int range, int AoE)
    {
        this.name = name;
        this.ID = id;
        this.setEffect(etype, eval);
        this.range = range;
        this.AoE = AoE;
    }
    public void setEffect(int etype, int eval)
    {
        this.effect = new Effect(etype, eval);
    }
    public void trigger(CreatureClass cc)
    {
        this.effect.affectCreature(cc);
    }
}

class Consumeable : Item
{

}

public class Effect
{
    int effectType;
    int e_value;

    public Effect(int effectType, int e_value)
    {
        this.effectType = effectType;
        this.e_value = e_value;
    }

    public void affectCreature(CreatureClass cc)
    {
        switch (effectType)
        {
            case 1: //Damage spell X
                cc.takeDamage(e_value);
                Debug.Log("(Spell)Damage on creature: " + cc.name);
                break;
            case 2: //Heal spell X
                cc.takeHealing(e_value);
                Debug.Log("(Spell)Heal on creature: " + cc.name);
                break;
            case 3: //Drain arcana X
                cc.spendArcana(e_value);
                Debug.Log("(Spell)Arcana drained on creature: " + cc.name);
                break;
            case 4: //Full arcana MAX
                cc.setArcana(cc.getMaxArcana());
                Debug.Log("(Spell)Arcana maxed for creature: " + cc.name);
                break;
            case 5: //Buff defense +1
                cc.setDefense(cc.getDefense() + 1f);
                Debug.Log("(Spell)Defense buffed for creature: " + cc.name);
                break;
            default:
                Debug.Log("spell effect hit default");
                break;
        }
    }

    void setType(int effectType)
    {
        this.effectType = effectType;
    }
    void setValue(int e_value)
    {
        this.e_value = e_value;
    }
}