using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroball : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
