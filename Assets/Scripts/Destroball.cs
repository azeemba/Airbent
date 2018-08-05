using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroball : BounceDestroAV {

    void Start()
    {
        GetComponent<Rigidbody2D>().sleepMode = RigidbodySleepMode2D.StartAsleep;
        base.Start();
    }

}
