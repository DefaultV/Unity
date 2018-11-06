using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraScript : NetworkBehaviour {

    public float panSpeed = 40f;
    public float panBorderThickness = 10f;
    public Vector2 limitClamp;

    public float scrlSpeed = 20f;
    public float minScrl = 20f;
    public float maxScrl = 120f;

    void Start()
    {
        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }
            
        gameObject.transform.Rotate(new Vector3(55,0,0));
        Camera.main.transform.SetParent(gameObject.transform);
        Camera.main.transform.localPosition = Vector3.zero;
        Camera.main.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void Update () {
        Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
            pos.z += panSpeed * Time.deltaTime;

        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
            pos.z -= panSpeed * Time.deltaTime;

        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
            pos.x -= panSpeed * Time.deltaTime;

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
            pos.x += panSpeed * Time.deltaTime;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrlSpeed * 100f * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -limitClamp.x, +limitClamp.x);
        pos.y = Mathf.Clamp(pos.y, minScrl, maxScrl);
        pos.z = Mathf.Clamp(pos.z, -limitClamp.y, +limitClamp.y);

        transform.position = pos;
	}
}
