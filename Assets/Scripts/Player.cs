using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int jumpTimesAllowed = 2;
    public int speed = 10;
    public int jumpForce = 10;
    public int maxVelX = 10;

    Rigidbody2D rigid;
    GameObject flipper;
    bool onGround = false;
    int jumpCount = 0;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        flipper = transform.Find("Flipper").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Vertical")) 
        {
            if (onGround || jumpCount < jumpTimesAllowed)
            {
                jumpCount++;
                rigid.velocity = new Vector2(rigid.velocity.x, 0f);
                rigid.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        float inAirMultiplier = onGround ? 1f : 0.5f;
        float horizontal = Input.GetAxis("Horizontal") * speed * inAirMultiplier;
        rigid.AddForce(transform.right * horizontal);

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("stop motor");
            HingeJoint2D joint = flipper.GetComponent<HingeJoint2D>();
            joint.useMotor = false;
            flipper.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            Debug.Log("restart motor");
            HingeJoint2D joint = flipper.GetComponent<HingeJoint2D>();
            joint.useMotor = true;
        }
        
        if (Mathf.Abs(rigid.velocity.x) > maxVelX)
        {
            Debug.Log("Trimming horizontal velocity");
            rigid.velocity = new Vector2(maxVelX * Mathf.Sign(rigid.velocity.x), rigid.velocity.y);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            EnterGroundState();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            ExitGroundState();
        }
    }

    private void EnterGroundState()
    {
        Debug.Log("On ground");
        onGround = true;
        jumpCount = 0;
    }

    private void ExitGroundState()
    {
        Debug.Log("Left ground");
        onGround = false;
    }
}
