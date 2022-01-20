using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point {
    public int time;
    public Vector3 position;
    public Vector3 rotation;
    public Point(int time, Transform gameobject) {
        this.time = time;
        this.position = gameobject.position;
        this.rotation = gameobject.localRotation.eulerAngles;
    }
}