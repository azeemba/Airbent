using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable: MonoBehaviour
{
    // Against a non bouncy surface, bounceCount is decremented.
    // Then hit count is decremented.
    // When hitCount = 0 and bounceCount=0, the object should be destroyed.
    public int bounceCount;
    public int hitCount;

    [HideInInspector]
    public bool isDamaged = false;

    private List<string> colliderTags = new List<string>() { "Wall", "Ground", "Ball" };

    void Start()
    {
    }

    public bool IsBouncy()
    {
        return (bounceCount > 0 || bounceCount == -1);
    }

    public bool IsDestroyed()
    {
        return (!IsBouncy() && hitCount == 0);
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
        Destroyable other = collision.collider.GetComponent<Destroyable>();

        if (other)
        {
            // Only handle our side of the collision
            if (!other.IsBouncy() || !IsBouncy())
            {
                RegisterBounceOrHit();
            }
        }
        else if (IsFromColliderTags(collision.collider))
        {
            RegisterBounceOrHit();
        }
    }

    void RegisterBounceOrHit()
    {
        if (IsBouncy())
        {
            bounceCount--;
        }
        else if (!IsDestroyed())
        {
            hitCount--;
            isDamaged = true;
        }
    }
}