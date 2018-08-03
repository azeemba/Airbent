using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroball : MonoBehaviour {

    private Rigidbody2D rigid;
    private Destroyable destroyableHandle;

	// Use this for initialization
	void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.sleepMode = RigidbodySleepMode2D.StartAsleep;

        destroyableHandle = GetComponent<Destroyable>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void HandleDestroy()
    {
        Destroy(gameObject);
    }

    public void Reset(Vector2 position)
    {
        rigid.sleepMode = RigidbodySleepMode2D.StartAsleep;
        transform.position = position;
        rigid.velocity = Vector2.zero;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (destroyableHandle.IsDestroyed())
        {
            HandleDestroy();
        }
    }
}
