using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

public class UnitController : NetworkBehaviour {

    public bool Selected = false;

    public GameObject lightSelect;
    SpriteRenderer l_select;
	void Start () {
        l_select = lightSelect.GetComponent<SpriteRenderer>();
        l_select.enabled = false;

        PeonGetIdentity();
    }
	
	void Update () {
        U_IsSelected();
        /*if (GetComponent<NavMeshAgent>().hasPath)
            if (GetComponent<NavMeshAgent>().remainingDistance <= 0.5f)
                GetComponentInChildren<Animator>().SetBool("Running", false);*/
    }

    void U_IsSelected()
    {
        if (Selected)
            if (!l_select.enabled)
                l_select.enabled = true;

        if (!Selected)
            if (l_select.enabled)
                l_select.enabled = false;
    }

    [SyncVar] public string PlayerID;
    [SyncVar] public int Health;
    [SyncVar] public int Armor;
    [SyncVar] public int AttackPower;
    [SyncVar] public float AttackSpeed;
    [SyncVar] public float MovementSpeed;

    void PeonGetIdentity()
    {
    }
}