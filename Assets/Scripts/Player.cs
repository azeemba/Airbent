using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int jumpTimesAllowed = 2;
    public int speed = 10;
    public int jumpForce = 10;

    Rigidbody2D rigid;
    bool onGround = false;
    int jumpCount = 0;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Vertical")) 
        {
            if (onGround || jumpCount < jumpTimesAllowed)
            {
                jumpCount++;
                rigid.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        float inAirMultiplier = onGround ? 1f : 0.5f;
        float horizontal = Input.GetAxis("Horizontal") * speed * inAirMultiplier;
        rigid.AddForce(transform.right * horizontal);
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
