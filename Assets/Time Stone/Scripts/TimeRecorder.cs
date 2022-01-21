using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeRecorder : MonoBehaviour
{
    public List<Point> allPointsInTime;
    public Rigidbody oldRB;
    public bool lastStatus = false;
    public TimeStone timeStone;
    void Start()
    {
        oldRB = transform.gameObject.GetComponent<Rigidbody>();
        allPointsInTime = new List<Point>();
        timeStone = GameObject.Find("Time Stone").GetComponent<TimeStone>();
        allPointsInTime.Add(new Point(-1, transform, transform.gameObject.GetComponent<Rigidbody>()));
        allPointsInTime.Add(new Point(0, transform, transform.gameObject.GetComponent<Rigidbody>()));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(allPointsInTime[allPointsInTime.Count-1].velocity);
        Debug.Log(allPointsInTime[allPointsInTime.Count-1].angularVelocity);
    }
    void FixedUpdate() {

        if(timeStone.isControlling) {

            transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
            transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            Point determinedSpot = getPointAtTime(timeStone.time);
            transform.position = determinedSpot.position;
            transform.rotation = determinedSpot.rotation;
            
            GetComponent<Rigidbody>().velocity = determinedSpot.velocity;
            GetComponent<Rigidbody>().angularVelocity = determinedSpot.angularVelocity;

        } else {

            transform.gameObject.GetComponent<Rigidbody>().useGravity = true;
            transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;

            while(allPointsInTime[allPointsInTime.Count-1].time > timeStone.time) {
                allPointsInTime.RemoveAt(allPointsInTime.Count-1);
            }

            if(!isPointSame(allPointsInTime[allPointsInTime.Count-1], transform)) {
                allPointsInTime.Add(new Point(timeStone.time, transform, transform.gameObject.GetComponent<Rigidbody>()));
            }
        }
    }

    Point getPointAtTime(int time) {
        for(int i = 0; i<allPointsInTime.Count; i++) {
            if(time < allPointsInTime[i].time) {
                return allPointsInTime[i-1];
            }
        }
        return allPointsInTime[allPointsInTime.Count-1];
    }


    bool isPointSame(Point object1, Transform object2) {
        return  object1.position == object2.position && 
                object1.rotation == object2.rotation;
    }
}
