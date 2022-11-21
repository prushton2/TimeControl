using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class TimeRecorder : MonoBehaviour
{
    public bool oldUseGravity;
    public bool oldIsKinematic;

    public bool lastStatus = false;
    public TimeController timeController; 

    public List<Point> allPointsInTime;

    public bool useExternalComponents = false;
    public AI attachedAI;


    void Start()
    {
        //remember variables that will change
        if(getrb() != null) {
            oldUseGravity  = transform.gameObject.GetComponent<Rigidbody>().useGravity;
            oldIsKinematic = transform.gameObject.GetComponent<Rigidbody>().isKinematic;
        }

        //init stuff
        allPointsInTime = new List<Point>();
        timeController = GameObject.Find("Time Controller").GetComponent<TimeController>();

        
        //add in default points to prevent errors with indexing
        allPointsInTime.Add(new Point(-1, transform, getrb(), null));
        allPointsInTime.Add(new Point(0, transform, getrb(), null));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate() {


        if(timeController.isControlling != lastStatus) { //if the timeController just changed, we have some work regarding saving some stats

            if(!timeController.isControlling) { //if the timeController just stopped controlling
            
                Point determinedSpot = getPointAtTime(timeController.time); //apply the forces to the object
                
                if(getrb() != null) { //tries exist to prevent error spam for objects without a rigidbody
                    GetComponent<Rigidbody>().velocity = determinedSpot.velocity;
                    GetComponent<Rigidbody>().angularVelocity = determinedSpot.angularVelocity;

                    transform.gameObject.GetComponent<Rigidbody>().useGravity  = oldUseGravity; //reset these values
                    transform.gameObject.GetComponent<Rigidbody>().isKinematic = oldIsKinematic;
                }

            } else { //if it just took control
                if(getrb() != null) {
                    oldUseGravity = transform.gameObject.GetComponent<Rigidbody>().useGravity; //save these values so they can be changed non destructively
                    oldIsKinematic  = transform.gameObject.GetComponent<Rigidbody>().isKinematic;

                    transform.gameObject.GetComponent<Rigidbody>().useGravity = false; //set them tpo what they need to be
                    transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                }
            }

        }

        lastStatus = timeController.isControlling; //how we know it was just changed

        if(timeController.isControlling) { //if the controller is in control

            Point determinedSpot = getPointAtTime(timeController.time); //go to the point the controller says
            transform.position = determinedSpot.position;
            transform.rotation = determinedSpot.rotation;

            if(useExternalComponents) {
                attachedAI.setData(determinedSpot.ai);
            }

            
        } else { //if it isnt in control

            while(allPointsInTime[allPointsInTime.Count-1].time > timeController.time) { //remove all points in the future
                allPointsInTime.RemoveAt(allPointsInTime.Count-1);
            }

            Point point = new Point(timeController.time, transform, getrb(), null);
            if(useExternalComponents) {
                point.ai = attachedAI.getData();
            }
                

            if(!allPointsInTime[allPointsInTime.Count-1].equals(point) ) { //if the object has moved, add the position to the list
                allPointsInTime.Add(point);
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


    Rigidbody getrb() { //returns null if no rb
        if(transform.gameObject.GetComponent<Rigidbody>() == null) {
            return null;
        } else {
            return transform.gameObject.GetComponent<Rigidbody>();
        }

    }

}
