using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeController : MonoBehaviour
{
    public bool isControlling;
    public bool justChanged;
    public int time = 0;
    public int latestTime = 0;
    public int now;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.GetChild(0).GetComponent<Light>().enabled = isControlling;
    }

    void FixedUpdate() {

        if(!isControlling) {
            time += 1;
            latestTime = time;
        }

        if(time<0) {
            time = 0;
        }


        justChanged = false;
    }

    public void setIsControlling(bool status) {
        isControlling = status;
        justChanged = true;
    }

    public void IncrementTime(int increment) {
        
        if(!isControlling) {return;}

        time += increment;

        time = Math.Clamp(time, 0, latestTime);
    }

}
