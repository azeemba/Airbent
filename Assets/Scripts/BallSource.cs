using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSource : MonoBehaviour {
    public GameObject ballPrefab;
    public ResetArea resetArea;

    private GameObject liveBall;

	// Use this for initialization
	void Start () {
        Reset();
        resetArea.ResetAreaEvent += HandleResetEvent;
	}

    private void Reset()
    {
        if (liveBall == null)
        {
            liveBall = (GameObject)Instantiate(ballPrefab, transform.position, Quaternion.identity);
        }
        else if (liveBall.transform.position != transform.position)
        {
            liveBall.GetComponent<Destroball>().Reset(transform.position);
        }
    }

    private void HandleResetEvent(ResetArea resetArea)
    {
        Reset();
    }
}
