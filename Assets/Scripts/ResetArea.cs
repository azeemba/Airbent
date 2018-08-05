using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ResetAreaEventHandler(ResetArea resetArea);

public class ResetArea : MonoBehaviour {
    public event ResetAreaEventHandler ResetAreaEvent; 

    public AudioClip resetAudio;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
            audioSource.PlayOneShot(resetAudio);
            TriggerResetAreaEvent();
        }
    }
}
