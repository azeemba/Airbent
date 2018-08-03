using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ResetAreaEventHandler(ResetArea resetArea);

public class ResetArea : MonoBehaviour {
    public event ResetAreaEventHandler ResetAreaEvent; 

    private void TriggerResetAreaEvent()
    {
        if (ResetAreaEvent != null)
        {
            ResetAreaEvent(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TriggerResetAreaEvent();
        }
    }
}
