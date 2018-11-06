using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBuilder : MonoBehaviour {
    public GameObject Creature;

    void Start () {
        GameObject Creature1 = Instantiate(Creature);
        GameObject Creature2 = Instantiate(Creature);
        GameObject Creature3 = Instantiate(Creature);
        Sword sword = new Sword("Wrath", 3, 0, 0);
        Bow bow = new Bow("Anger", 0, 0, 3, 2);

        Creature1.GetComponent<CreatureController>().SetCreaturePosition(1, 4);
        Creature1.GetComponent<CreatureClass>().SetupNewCreature(true, 1, "Kjade", 110, 50, 20, 1, 4, 12, 1, 2, 45);
        Creature1.GetComponent<CreatureClass>().setMainHandSword(sword);
        Creature1.name = "Kjade";

        Creature2.GetComponent<CreatureController>().SetCreaturePosition(2, 2);
        Creature2.GetComponent<CreatureClass>().SetupNewCreature(true, 1, "Magnus", 150, 5, 10, 2, 3, 8, 1, 4, 5);
        Creature2.GetComponent<CreatureClass>().setMainHandBow(bow);
        Creature2.name = "Magnus";

        Creature3.GetComponent<CreatureController>().SetCreaturePosition(2, 4);
        Creature3.GetComponent<CreatureClass>().SetupNewCreature(true, 1, "Jens the mad", 150, 5, 10, 3, 3, 8, 1, 4, 5);
        Creature3.name = "Jens the mad";
    }
	

	void Update () {
		
	}
}
