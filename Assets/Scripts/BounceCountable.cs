using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void BounceResetHandler(int hp);
public delegate void BounceHitHandler(int newHp);
public delegate void BounceDestroyedHandler();

public class BounceCountable: MonoBehaviour
{
    // -1 means infinite
    public int hitCount;

    public event BounceHitHandler BounceHitEvent;
    public event BounceDestroyedHandler BounceDestroyEvent;

    [HideInInspector]
    public bool isDamaged = false;

    private readonly List<string> colliderTags = new List<string>() { "Wall", "Ground", "Ball" };
    private int startupHp = 0;

    void Start()
    {
        startupHp = hitCount;
        isDamaged = false;
    }

    public bool IsDestroyed()
    {
        return (hitCount == 0);
    }

    private bool IsFromColliderTags(Collider2D other)
    {
        foreach (string s in colliderTags)
        {
            if (other.CompareTag(s))
            {
                return true;
            }
        }
        return false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        BounceCountable other = collision.collider.GetComponent<BounceCountable>();

        // if the other thing is destroyable or it is tagged 
        if (other || IsFromColliderTags(collision.collider))
        {
            RegisterBounceOrHit();
        }
    }

    void RegisterBounceOrHit()
    {
        if (hitCount > 0)
        {
            hitCount--;
            isDamaged = true;

            if (hitCount == 0)
            {
                TriggerBounceDestroyed();
            }
            else
            {
                TriggerBounceHitEvent(hitCount);
            }
        }
    }

    private void TriggerBounceHitEvent(int reportedHp)
    {
        if (BounceHitEvent != null)
        {
            BounceHitEvent(reportedHp);
        }
    }

    private void TriggerBounceDestroyed()
    {
        if (BounceDestroyEvent != null)
        {
            BounceDestroyEvent();
        }
    }
}