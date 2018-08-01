using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrowall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            Debug.Log("Ball hit ground or wall");
            Destroy(gameObject);
        }
    }
}
