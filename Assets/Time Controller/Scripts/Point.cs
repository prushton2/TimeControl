using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class Point {
    public int time;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;
    public Vector3 angularVelocity;
    public JObject ai;
    public JObject healthPool;

    public Point(int time, Transform transform, Rigidbody rb, JObject ai = null, JObject healthPool = null) {
        this.time = time;
        
        this.position = transform.position;
        this.rotation = transform.rotation;
        
        this.ai = ai;

        this.healthPool = healthPool;

        if(rb != null) {
            this.velocity = rb.velocity;
            this.angularVelocity = rb.angularVelocity;
        } else {
            this.velocity = new Vector3(0, 0, 0);
            this.angularVelocity = new Vector3(0, 0, 0);
        }

    }

    public bool equals(Point other) {
        if(this.position != other.position || this.rotation != other.rotation) {
            return false;
        }

        if(this.ai != other.ai) {
            return false;
        }

        if(this.healthPool != other.healthPool) {
            return false;
        }

        return true;
    }
}