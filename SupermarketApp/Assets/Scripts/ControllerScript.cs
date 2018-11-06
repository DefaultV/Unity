using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour {

    
    public GameObject i_arrow;
    float arrowCooldown = 1f;

	// Use this for initialization
	void Start () {
        //fuck the cursor
        Cursor.visible = false;

    }
	
	// Update is called once per frame
	void Update () {
        CharacterControls();
        SpawnArrow();
	}


    //FUNCS
    void CharacterControls()
    {
        Vector3 mov = new Vector3(Input.GetAxis("Horizontal") * 5f, 0, Input.GetAxis("Vertical") * 5f);

        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        transform.GetChild(0).Rotate(-Input.GetAxis("Mouse Y"), 0, 0);

        mov = transform.rotation * mov;
        GetComponent<CharacterController>().Move(mov * Time.deltaTime);
    }

    void SpawnArrow()
    {
        arrowCooldown -= Time.deltaTime;
        if (arrowCooldown <= 0f)
        {
            Instantiate(i_arrow);
            //i_arrow.transform.position = transform.position;
            i_arrow.transform.position = new Vector3(transform.position.x, 0.06f, transform.position.z);

            arrowCooldown = 0.5f;
        }
    }
}
