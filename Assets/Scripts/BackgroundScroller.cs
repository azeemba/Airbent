using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    public GameObject Player;
    public List<GameObject> scrollerBg;

    public Sprite randomDrops;

    private Vector2 offset;
    private List<Vector2> scrollerOffset;

	// Use this for initialization
	void Start ()
    {
        scrollerOffset = new List<Vector2>();
        offset = Player.transform.position;
        foreach (GameObject bg in scrollerBg)
        {
            scrollerOffset.Add(bg.transform.position);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        float curOffset = (Player.transform.position.x - offset.x); 
        for(int i = 0; i < scrollerBg.Count; ++i)
        {
            scrollerBg[i].transform.position = scrollerOffset[i] + Vector2.right*curOffset;
        }
	}
}
