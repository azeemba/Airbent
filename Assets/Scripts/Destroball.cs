using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroball : MonoBehaviour {

    private bool frozen;
    private Rigidbody2D rigid;

	// Use this for initialization
	void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.sleepMode = RigidbodySleepMode2D.StartAsleep;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Reset(Vector2 position)
    {
        rigid.sleepMode = RigidbodySleepMode2D.StartAsleep;
        transform.position = position;
        rigid.velocity = Vector2.zero;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") ||
            collision.collider.CompareTag("Wall"))
        {
            Debug.Log("Ball hit ground or wall");
            Destroy(gameObject);
        }
    }
}
