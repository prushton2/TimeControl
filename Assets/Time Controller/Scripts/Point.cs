using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point {
    public int time;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;
    public Vector3 angularVelocity;
    public List<string> componentData = new List<string>();

    public Point(int time, Transform gameobject, Rigidbody rb, List<string> componentData = null) {
        this.time = time;

        this.position = gameobject.position;
        this.rotation = gameobject.rotation;
        
        this.componentData = componentData;

        if(rb != null) {
            this.velocity = rb.velocity;
            this.angularVelocity = rb.angularVelocity;
        } else {
            this.velocity = new Vector3(0, 0, 0);
            this.angularVelocity = new Vector3(0, 0, 0);
        }

    }
}