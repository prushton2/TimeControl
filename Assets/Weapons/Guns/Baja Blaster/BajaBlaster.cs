using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BajaBlaster : Gun
{
    // Start is called before the first frame update
    
    
    void Start()
    {
        base.damage = 122166;
        base.magazine = 7;
        base.timeToRecharge = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
