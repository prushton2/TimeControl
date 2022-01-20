using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point {
    public int time;
    public Transform gameObject;
    public Point(int time, Transform gameobject) {
        this.time = time;
        this.gameObject = gameObject;
    }
}