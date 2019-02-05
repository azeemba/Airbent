using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceDestroAV : MonoBehaviour {
    private BounceCountable bounceCountable;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    public List<Sprite> spriteByHP;
    public List<Sprite> spriteDestro;
    public AudioClip bounceAudio;
    public AudioClip destroyAudio;
    public float timePerDestroFrame = 0.1f;

	// Use this for initialization
	protected void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();

        bounceCountable = GetComponent<BounceCountable>();
        bounceCountable.BounceDestroyEvent += HandleDestroy;
        bounceCountable.BounceHitEvent += HandleHit;

        UpdateSprite(bounceCountable.hitCount);
	}

    private void HandleDestroy()
    {
        if (destroyAudio != null)
        {
            //audioSource.PlayOneShot(destroyAudio);
        }
        StartCoroutine(AnimateDestroy());
    }

    private IEnumerator AnimateDestroy()
    {
        float alphaSteps = 5f;
        float i = 0;
        Color startColor = spriteRenderer.color;
        Color transparent = new Color(
            startColor.r, startColor.g, startColor.b, 0);
        foreach (Sprite s in spriteDestro)
        {
            spriteRenderer.sprite = s;
            // spriteRenderer.color = Color.Lerp(
            //    Color.white, transparent, i/alphaSteps);
            // i++;
            yield return new WaitForSeconds(timePerDestroFrame);
        }

        while (i < alphaSteps)
        {
            spriteRenderer.color = Color.Lerp(
                startColor, transparent, i/alphaSteps);
            i++;
            yield return new WaitForSeconds(timePerDestroFrame);
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
        if (bounceAudio != null)
        {
            audioSource.PlayOneShot(bounceAudio);
        }
        UpdateSprite(newHp);
    }



}
