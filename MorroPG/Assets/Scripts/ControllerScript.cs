using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{

    public GameObject Eyes;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        CharacterControls();
    }
    
    void CharacterControls()
    {

        Vector3 mov = new Vector3(Input.GetAxis("Horizontal") * 5f, 0, Input.GetAxis("Vertical") * 5f);

        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        Eyes.transform.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);

        mov = transform.rotation * mov;
        GetComponent<CharacterController>().Move(mov * Time.deltaTime);
    }
}
