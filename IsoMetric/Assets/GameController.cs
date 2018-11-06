using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public selectorController SC;
    public UIController UIC;
    public SoundBox SB;
    GameObject[] creatureList;
    CreatureController currentcreature;
    int currentIndex;
    int actionPoints;

    void Start () {
		
	}
	

	void Update () {
		
	}

    public void SetCreatureList(GameObject[] list)
    {
        currentIndex = 0;
        this.creatureList = list;
        currentcreature = creatureList[currentIndex].GetComponent<CreatureController>();
        resetPoints();
    }

    public void resetPoints()
    {
        this.actionPoints = 2;
        UpdatePoints();
    }

    public void actionPointSpent()
    {
        actionPoints--;
        UpdatePoints();
    }

    public void actionPointRecieve()
    {
        actionPoints++;
        UpdatePoints();
    }

    public int getActionPoints()
    {
        return this.actionPoints;
    }

    public void NextCreatureInQueue()
    {
        currentIndex++;
        if (currentIndex == creatureList.Length)
            currentIndex = 0;
        currentcreature = creatureList[currentIndex].GetComponent<CreatureController>();
        resetPoints();
        SwitchTurn();
    }

    void SwitchTurn()
    {
        SC.SetSelectorPosition(currentcreature.getPosX(), currentcreature.getPosY());
    }

    void UpdatePoints()
    {
        UIC.UpdateActionPoints(this.actionPoints);
    }
}
