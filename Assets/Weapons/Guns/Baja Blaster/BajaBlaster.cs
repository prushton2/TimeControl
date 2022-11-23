using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BajaBlaster : Gun
{
    // Start is called before the first frame update
    
    new void Start()
    {
        damage = 1500;
        magazine = 7;
        timeToRecharge = 50;
        minTimeToRecharge = 5;
        range = 25;
        bulletLifespan = 2;
        discountIterator = 5;
        base.Start();
    }

    new void FixedUpdate() {
        base.FixedUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
