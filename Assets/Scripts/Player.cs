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
    HingeJoint2D flipperJoint;
    bool onGround = false;
    int jumpCount = 0;
    bool flipperPaused = false;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        flipper = transform.Find("Flipper").gameObject;
        flipperJoint = flipper.GetComponent<HingeJoint2D>();

        flipperJoint.useMotor = true;
	}
	
	// Update is called once per frame
    // Conceptual mechanics : move left-right, jump, double jump, climb, pinch
    // Control Mechanics: left-right, jump, double jump, pause/start rotation
	void Update () {
        if (Input.GetButtonDown("Vertical")) 
        {
            if (onGround || jumpCount < jumpTimesAllowed)
            {
                jumpCount++;
                rigid.velocity = new Vector2(rigid.velocity.x, 0f);
                float secondJumpPenalty = jumpCount > 0 ? 0.75f : 1f;
                rigid.AddForce(transform.up * jumpForce * secondJumpPenalty, ForceMode2D.Impulse);
            }
        }

        float inAirMultiplier = onGround ? 1f : 0.5f;
        float horizontal = Input.GetAxis("Horizontal") * speed * inAirMultiplier;
        rigid.AddForce(transform.right * horizontal);

        if (Input.GetButtonDown("Fire1"))
        {
            StopFlipper();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            RestartFlipper();
        }
        
        if (Mathf.Abs(rigid.velocity.x) > maxVelX)
        {
            Debug.Log("Trimming horizontal velocity");
            rigid.velocity = new Vector2(maxVelX * Mathf.Sign(rigid.velocity.x), rigid.velocity.y);
        }

        if (!flipperPaused &&
            Mathf.Abs(
                flipper.GetComponent<Rigidbody2D>().angularVelocity) < 0.001f)
        {
            Debug.Log("Uh...is everything okay?");
            Debug.Log(flipper.GetComponent<Rigidbody2D>().angularVelocity);
            StopFlipper();
            StartCoroutine(RestartFlipperWithDelay(0.5f));
        }
	}

    private void StopFlipper()
    {
        Debug.Log("stop motor");
        flipperJoint.useMotor = false;
        flipperPaused = true;
        flipper.GetComponent<Rigidbody2D>().angularVelocity = 0f;
    }

    private void RestartFlipper()
    {
        Debug.Log("restart motor");
        flipperJoint.useMotor = true;
        flipperPaused = false;
    }

    private IEnumerator RestartFlipperWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RestartFlipper();
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
