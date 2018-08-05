using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrowall : MonoBehaviour {

    private BounceCountable bounceCountable;

	// Use this for initialization
	void Start ()
    {
        bounceCountable = GetComponent<BounceCountable>();

        bounceCountable.BounceDestroyEvent += HandleDestroy;
        bounceCountable.BounceHitEvent += HandleHit;
	}
	
    private void HandleDestroy()
    {
        Destroy(gameObject);
    }

    private void HandleHit(int newHp)
    {
    }
}
