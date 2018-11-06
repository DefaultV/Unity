using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    // Use this for initialization
    private Rigidbody2D rb2D; //the Rigidbody inside our object 
    private CircleCollider2D col2D;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();  // Getting Access to the Rigidbody 
        col2D = GetComponent<CircleCollider2D>();
    }
    public KeyCode RightArrow;
    public KeyCode LeftArrow;
    public KeyCode Space;
    public float Speed = 0.0f;
    public float Jump = 0.0f;
    // Update is called once per frame
    void Update () {
        /*if (col2D.IsTouchingLayers(1))
        {

        }*/
		if (Input.GetKey(RightArrow))
        {
            Speed += 0.03f;
            
        }
        else if (Speed > 0.0f && !Input.GetKey(LeftArrow))
        {
            Speed -= 0.01f;
        }
        if (Input.GetKey(LeftArrow))
        {
            Speed -= 0.03f;

        }
        else if (Speed < 0.0f && !Input.GetKey(RightArrow))
        {
            Speed += 0.01f;
        }

        if (Input.GetKey(Space))
        {
            Jump += 0.5f;
            
        } else if (Jump > 0)
        {
            Jump -= 0.10f;
        }
        rb2D.velocity = new Vector2(Speed, Jump);
    }
}
