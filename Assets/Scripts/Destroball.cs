using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroball : MonoBehaviour {

    private Rigidbody2D rigid;
    private BounceCountable bounceCountable;
    private SpriteRenderer spriteRenderer;

    public List<Sprite> spriteByHP;

	// Use this for initialization
	void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.sleepMode = RigidbodySleepMode2D.StartAsleep;

        spriteRenderer = GetComponent<SpriteRenderer>();

        bounceCountable = GetComponent<BounceCountable>();

        bounceCountable.BounceDestroyEvent += HandleDestroy;
        bounceCountable.BounceHitEvent += HandleHit;

        UpdateSprite(bounceCountable.hitCount);
	}
	
    private void HandleDestroy()
    {
        Destroy(gameObject);
    }

    private void UpdateSprite(int newHp)
    {
        if (newHp <= 0)
        {
            //destroy will be called
            return;
        }
        else if (newHp > spriteByHP.Count)
        {
            // I am going to wait for HP to get to my level
            return;
        }
        else
        {
            spriteRenderer.sprite = spriteByHP[newHp - 1];
        }
    }
    private void HandleHit(int newHp)
    {
        UpdateSprite(newHp);
    }

}
