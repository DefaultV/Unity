using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camController : MonoBehaviour {
	private const float Y_ANGLE_MIN = -89.0f;
	private const float Y_ANGLE_MAX = 89.0f ;

	public Transform lookAt;
	public Transform ply;
	public Transform camTransform;
	public bool looking;

	private Camera cam;

	private float scrl = 0.0f;
	private float distance = 10.0f;
	private float currentX = 0.0f;
	private float currentY = 0.0f;
	private float sensivityX = 4.0f;
	private float sensivityY = 1.0f;

	private void start ()
	{
		camTransform = Camera.main.transform;
		cam = Camera.main;
	}

	private void Update()
	{
		
	}

	public float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;

	private void LateUpdate()
	{
		cameraWork ();
		camDistance ();
	}

	private void cameraWork(){
		Vector3 dir;
		Quaternion rotation;


		if (Input.GetKey(KeyCode.Mouse1)){
			looking = true;
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.lockState = CursorLockMode.Confined;

			doCamAngles (false);

			rotation = Quaternion.Euler (-currentY, currentX, 0);
			dir = new Vector3 (0, 0, -distance);

			camTransform.position = lookAt.position + rotation * dir;
			camTransform.LookAt(lookAt.position);
		}
		else{
			looking = false;
			//doCamAngles (true);
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;


			if (ply.GetComponent<PlyController> ().isMoving){
				rotation = Quaternion.Euler (-currentY, camTransform.rotation.eulerAngles.x, 0);//camTransform.rotation; //(Camera.main.transform.rotation.x, Camera.main.transform.rotation.y, Camera.main.transform.rotation.z);
				dir = new Vector3 (0, 0, -distance);
				camTransform.position = Vector3.SmoothDamp (camTransform.position, lookAt.position + rotation * dir, ref velocity, smoothTime);
				camTransform.LookAt(lookAt.position);
			}
			else{
				rotation = Quaternion.Euler (0, 0, 0);
				dir = new Vector3 (0, 0, 0);
				//camTransform.position = Vector3.SmoothDamp (camTransform.position, lookAt.position + rotation * dir, ref velocity, smoothTime);
			}

		}
	}

	private void doCamAngles(bool reset){
		if (reset){
			currentX = 0.0f;
			//currentY = 0.0f;
		}
		currentX += Input.GetAxis ("Mouse X");
		currentY += Input.GetAxis ("Mouse Y");

		currentY = Mathf.Clamp(currentY,Y_ANGLE_MIN,Y_ANGLE_MAX);
	}
	private void camDistance(){
		scrl = Input.GetAxis ("Mouse ScrollWheel");
		if (scrl > 0.0f){
			//up
			distance = distance - 1.0f;
		}
		else if (scrl < 0.0f){
			//down
			distance = distance + 1.0f;
		}
	}
}
