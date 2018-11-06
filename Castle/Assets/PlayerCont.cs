using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCont : MonoBehaviour {
	public AudioClip jumpsound;
	public CapsuleCollider2D coll2d;
	public GameObject SmokePrefab;
	public bool onGround;
	public KeyCode RightArrow;
	public KeyCode LeftArrow;
	public KeyCode Space;
	public KeyCode Attack;
	public float Speed = 0.0f;
	public float Jump = 0.0f;
	public Animator anim;
	int trigAnimWalk = 0;
	bool faceright = true;
	bool justJumped = false;
	bool justLanded = false;
	float Cooldown;
	void Start()
	{
	}

	void Update () {
		Cooldown += Time.deltaTime;
		Vector3 m_scale = transform.localScale;
		onGround = coll2d.IsTouchingLayers();
		if (Input.GetKeyDown (Attack)) {
			anim.SetBool ("Attacking", true);
			print ("LOL");
		} else {
			anim.SetBool ("Attacking", false);
		}

		if (Input.GetKey(RightArrow))
		{
			trigAnimWalk = 1;
			Speed = 2.0f * Time.deltaTime;
		}
		else if (Speed > 0.0f && !Input.GetKey(LeftArrow))
		{
			trigAnimWalk = 0;
			Speed = 0.0f * Time.deltaTime;
		}
		if (Input.GetKey(LeftArrow))
		{
			trigAnimWalk = 1;
			Speed = -2.0f * Time.deltaTime;

		}
		else if (Speed < 0.0f && !Input.GetKey(RightArrow))
		{
			trigAnimWalk = 0;
			Speed = 0.0f * Time.deltaTime;
		}

		if (Input.GetKeyDown (Space) && onGround) {
			if (Jump <= 0.0f) {
				Jump = 0.046f;
				justJumped = true;
			}
		} 
		if (Jump > 0) {
			Jump -= 0.10f * Time.deltaTime;
		}
		if (onGround) {
			anim.SetBool ("Jump", false);
			if (justJumped) {
				SpawnSmoke ();
				GetComponent<AudioSource>().PlayOneShot(jumpsound);
				justJumped = false;
				justLanded = true;
				Cooldown = -0.5f;
			}
		}
		else {
			anim.SetBool ("Jump", true);
		}

		if (justLanded && Cooldown >= 0) {
			if (onGround) {
				SpawnSmoke ();
				GetComponent<AudioSource>().PlayOneShot(jumpsound);
				justLanded = false;
			}
		}
			
		if (trigAnimWalk == 1) {
			anim.SetBool ("Walking", true);
		} 
		else {
			anim.SetBool ("Walking", false);
		}

		transform.Translate(new Vector2(Speed, Jump));
	}
	void SpawnSmoke(){
		Instantiate (SmokePrefab, (new Vector3 (transform.position.x, transform.position.y - 0.5f, transform.position.z)), Quaternion.identity);
		Instantiate (SmokePrefab, (new Vector3 (transform.position.x-0.4f, transform.position.y - 0.5f, transform.position.z)), Quaternion.identity);
		Instantiate (SmokePrefab, (new Vector3 (transform.position.x+0.3f, transform.position.y - 0.5f, transform.position.z)), Quaternion.identity);
	}
}
