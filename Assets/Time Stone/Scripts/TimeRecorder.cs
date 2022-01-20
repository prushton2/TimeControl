using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeRecorder : MonoBehaviour
{
    public List<Point> allPointsInTime;
    public TimeStone timeStone;
    // Start is called before the first frame update
    void Start()
    {
        allPointsInTime = new List<Point>();
        timeStone = GameObject.Find("Time Stone").GetComponent<TimeStone>();
        allPointsInTime.Add(new Point(-1, transform));
        allPointsInTime.Add(new Point(timeStone.time, transform));
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate() {
        
        toggleAllElements(!timeStone.isControlling);

        if(timeStone.isControlling) {
            Point determinedSpot = getPointAtTime(timeStone.time);
            transform.position = determinedSpot.position;
            transform.rotation = Quaternion.Euler(determinedSpot.rotation);
        } else {
            while(allPointsInTime[allPointsInTime.Count-1].time > timeStone.time) {
                allPointsInTime.RemoveAt(allPointsInTime.Count-1);
            }
            if(allPointsInTime[allPointsInTime.Count-1].position != transform.position || allPointsInTime[allPointsInTime.Count-1].rotation != transform.localRotation.eulerAngles) {
                
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
}
