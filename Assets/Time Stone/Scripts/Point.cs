using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point {
    public int time;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;
    public Vector3 angularVelocity;

    public Point(int time, Transform gameobject, Rigidbody rb) {
        this.time = time;

        this.position = gameobject.position;
        this.rotation = gameobject.rotation;

        this.velocity = rb.velocity;
        this.angularVelocity = rb.angularVelocity;
    }
}