using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroSource: MonoBehaviour {
    public GameObject destroPrefab;
    public ResetArea resetArea;

    private GameObject destroLiveObj;

	// Use this for initialization
	void Start ()
    {
        Reset();
        resetArea.ResetAreaEvent += HandleResetEvent;
	}

    private void Reset()
    {
        if (destroLiveObj != null)
        {
            Destroy(destroLiveObj);
        }

        destroLiveObj = (GameObject)Instantiate(destroPrefab, transform.position, Quaternion.identity);
    }

    private void HandleResetEvent(ResetArea resetArea)
    {
        Reset();
    }
}
