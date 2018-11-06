using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

public class PlayerController : NetworkBehaviour {

    Ray ray;
    RaycastHit hit;
//    UnitSelector US;

	// Use this for initialization
	void Start () {
        if (!isLocalPlayer)
            return;

        //US = GetComponent<UnitSelector>();
	}
	
	// Update is called once per frame
	void Update () {
        //U_SelectUnit();
        if (!isLocalPlayer)
            return;
        CmdChooseBase();
    }

    public GameObject baseViewPrefab;
    public GameObject basePrefab;
    GameObject baseInstance;
    bool isChoosing = false;
    bool baseLocationChosen = false;


    //Choose base
    void CmdChooseBase()
    {
        if (Input.GetKeyDown(KeyCode.B) && !isChoosing && !baseLocationChosen)
        {
            baseInstance = Instantiate(baseViewPrefab);
            baseInstance.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isChoosing = true;
        }
        if (isChoosing)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                baseInstance.transform.position = hit.point;
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                isChoosing = false;
                baseLocationChosen = true;
                CmdSpawnBase(baseInstance.transform.position);
                Destroy(baseInstance);
                //baseInstance.gameObject.SetActive(false);
            }
        }
    }

    [Command]
    void CmdSpawnBase(Vector3 coordinates)
    {
        GameObject instance = Instantiate(basePrefab);
        instance.transform.position = coordinates;
        instance.GetComponent<BaseController>().baseNetID = transform.name;
        NetworkServer.Spawn(instance);
    }
}
