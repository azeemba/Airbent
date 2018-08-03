using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrowall : MonoBehaviour {

    private Destroyable destroyableHandle;

	// Use this for initialization
	void Start ()
    {
        destroyableHandle = GetComponent<Destroyable>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void HandleDestroy()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (destroyableHandle.IsDestroyed())
        {
            HandleDestroy();
        }
    }
}
