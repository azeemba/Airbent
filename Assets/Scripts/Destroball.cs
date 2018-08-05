using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroball : MonoBehaviour {

    private Rigidbody2D rigid;
    private BounceCountable bounceCountable;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    public List<Sprite> spriteByHP;
    public List<Sprite> spriteDestro;
    public AudioClip bounceAudio;
    public AudioClip destroyAudio;

	// Use this for initialization
	void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.sleepMode = RigidbodySleepMode2D.StartAsleep;

        spriteRenderer = GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();

        bounceCountable = GetComponent<BounceCountable>();
        bounceCountable.BounceDestroyEvent += HandleDestroy;
        bounceCountable.BounceHitEvent += HandleHit;

        UpdateSprite(bounceCountable.hitCount);
	}
	
    private void HandleDestroy()
    {
        audioSource.PlayOneShot(destroyAudio);
        StartCoroutine(AnimateDestroy());
    }

    private IEnumerator AnimateDestroy()
    {
        float alphaSteps = 5f;
        float i = 0;
        Color transparent = new Color(1, 1, 1, 0);
        foreach (Sprite s in spriteDestro)
        {
            spriteRenderer.sprite = s;
            spriteRenderer.color = Color.Lerp(
                Color.white, transparent, i/alphaSteps);
            i++;
            yield return new WaitForSeconds(0.1f);
        }

        while (i < alphaSteps)
        {
            spriteRenderer.color = Color.Lerp(
                Color.white, transparent, i/alphaSteps);
            i++;
            yield return new WaitForSeconds(0.1f);
        }

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
        audioSource.PlayOneShot(bounceAudio);
        UpdateSprite(newHp);
    }

}
