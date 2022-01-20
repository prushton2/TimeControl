using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeRecorder : MonoBehaviour
{
    public List<Point> allPointsInTime;
    public List<string> allPointsFiltered;
    public TimeStone timeStone;
    // Start is called before the first frame update
    void Start()
    {
        allPointsInTime = new List<Point>();
        timeStone = GameObject.Find("Time Stone").GetComponent<TimeStone>();
        allPointsInTime.Add(new Point(-1, transform));
        allPointsInTime.Add(new Point(0, transform));
    }

    // Update is called once per frame
    void Update()
    {
        allPointsFiltered.Clear();
        foreach (Point point in allPointsInTime) {
            allPointsFiltered.Add("Time:"+point.time);
        }
    }
    void FixedUpdate() {
        
        toggleAllElements(!timeStone.isControlling);

        if(timeStone.isControlling) {
            Point determinedSpot = getPointAtTime(timeStone.time);
            Debug.Log(determinedSpot.gameObject.position);
            // transform.position = determinedSpot.gameObject.position;
            // transform.position = determinedSpot.gameObject.position;
        } else {
            while(allPointsInTime[allPointsInTime.Count-1].time > timeStone.time) {
                allPointsInTime.RemoveAt(allPointsInTime.Count-1);
            }
            if(!isObjectSame(allPointsInTime[allPointsInTime.Count-1].gameObject, transform)) {
                Debug.Log("added");
                allPointsInTime.Add(new Point(timeStone.time, transform));
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

    void toggleAllElements(bool status) {
        transform.gameObject.GetComponent<Rigidbody>().useGravity = status;
        transform.gameObject.GetComponent<Rigidbody>().isKinematic = !status;
    }

    bool isObjectSame(Transform object1, Transform object2) {
        return  object1.position == object2.position && 
                object1.rotation == object2.rotation;
    }
}
