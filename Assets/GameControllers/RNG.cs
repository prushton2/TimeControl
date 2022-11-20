using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RNG : MonoBehaviour
{
    // Start is called before the first frame update
    
    public int seed;
    public int secondSeed;
    public int response;

    private System.Random brnd = new System.Random();
    public System.Random rnd;

    void Start()
    {
        seed = brnd.Next(1,99999);
    }

    void Update() {
        response = Next(this.secondSeed);

    }

    // Update is called once per frame
    public int Next(int secondSeed) { // same index and seed will always return the same number. Time travel friendly RNG!
        
        rnd = new System.Random(seed);

        rnd = new System.Random(rnd.Next() * (secondSeed+1));
        
        return rnd.Next();
    }
}
