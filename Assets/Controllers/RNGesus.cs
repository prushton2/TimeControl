using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RNGesus : MonoBehaviour
{
    // Start is called before the first frame update
    
    public int seed;
    public int secondSeed;
    public int response;

    void Start()
    {
        System.Random rnd = new System.Random();
        seed = rnd.Next(1,99999);
    }

    void Update() {
        response = Next(this.secondSeed);

    }

    // Update is called once per frame
    public int Next(int secondSeed) { // same index and seed will always return the same number. Time travel friendly RNG!
        return secondSeed * seed;
    }
}
